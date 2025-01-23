using configApp.Actions;
using configApp.ConfigToolbar;
using configApp.Controls;
using configApp.JsonConverters;
using configApp.UI;
using System.Configuration;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace configApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            //WriteIndented = true

        };

        private IInputButton? _activeButton = null;
        private SerialPort _serialPort;
        private ConnectDeviceSerialWindow _connectDeviceSerialWindow;

        public string json = "";

        public MainWindow(SerialPort serialPort, ConnectDeviceSerialWindow connectDeviceSerialWindow)
        {
            _serialPort = serialPort;
            _connectDeviceSerialWindow = connectDeviceSerialWindow;

            InitializeComponent();
            options.Converters.Add(new IControlListJsonConverter());
            options.Converters.Add(new MacroActionJsonConverter());
            options.Converters.Add(new KeyboardActionJsonConverter());
            options.Converters.Add(new ButtonControlJsonConverter());
            options.Converters.Add(new DelayActionJsonConverter());
            options.Converters.Add(new IActionJsonConverter());
            options.Converters.Add(new EncoderControlJsonConverter());

            if (!InitializeConfiguration())
            {
                // Jeśli nie udało się pobrać konfiguracji, zamykamy okno
                MessageBox.Show("Failed to retrieve configuration. Closing the application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private bool InitializeConfiguration()
        {

            //TODO add verification of type of device
            _connectDeviceSerialWindow.ConnectivityCheck = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    MessageBox.Show("Serial port is not open. Unable to retrieve configuration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                _serialPort.ReadTimeout = 5000;
                _serialPort.Write("getConfiguration");

                string response = _serialPort.ReadLine();

                ProcessConfiguration(response);
                _connectDeviceSerialWindow.ConnectivityCheck = true;

                return true;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Timeout while waiting for configuration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ProcessConfiguration(string json)
        {
            try
            {
                List<IControl> config = JsonSerializer.Deserialize<List<IControl>>(json, options);
                if (config != null && config.Count == 17)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (ButtonsMatrixGrid.Children[i] is IInputButton button)
                        {
                            button.SavedControl = config[i];
                        }
                    }

                    EncoderElement.SavedControl = config[16];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid configuration format: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
            ConnectDeviceSerialWindow window = new ConnectDeviceSerialWindow();
            window.Show();
        }

        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is KeyButton keyButton)
            {

                if (_activeButton == keyButton)
                {
                    ResetCanvas();
                    _activeButton = null;
                }
                else
                {
                    _activeButton = keyButton;
                    UpdateCanvas();
                }
            }
        }

        private void KeyButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is IInputButton button)
            {
                if (_activeButton != null)
                {
                    _activeButton.IsChecked = false;
                }
                _activeButton = button;
                UpdateCanvas();
            }
        }

        private void KeyButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is KeyButton keyButton)
            {
                _activeButton = null;
                Console.WriteLine($"Przycisk {keyButton.Content} wyłączony.");
                ResetCanvas(); 
            }
        }

        private void UpdateCanvas()
        {
            NotSelectedElementConfig.Visibility = Visibility.Hidden;
            BasicToolbar toolbar = new BasicToolbar()
            {
                Control = _activeButton.SavedControl,
                
            };
            toolbar.SaveClicked += (s, e) => SaveButton_Click(s, e);
            ToolbarSectionGrid.Children.Clear();
            ToolbarSectionGrid.Children.Add(toolbar);
        }

        private void ResetCanvas()
        {
            ToolbarSectionGrid.Children.Clear();
            NotSelectedElementConfig.Visibility = Visibility.Visible;
        }

        //TODO Make sure this method is in right class. Probably it is
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _connectDeviceSerialWindow.ConnectivityCheck = false;
            Thread.Sleep(1000);
            _activeButton.SavedControl = ((BasicToolbar)sender).Control;

            List<IControl> controls = ButtonsMatrixGrid.Children.OfType<IInputButton>().Select(b => b.SavedControl).ToList();
            controls.Add(EncoderElement.SavedControl);

            json = JsonSerializer.Serialize(controls, options);
            Debug.WriteLine(json);

            string response;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    MessageBox.Show("Serial port is not open. Unable to save configuration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int oldTimeout = _serialPort.ReadTimeout;
                _serialPort.Write("setConfiguration");
                //_serialPort.WriteLine(json);

                response = _serialPort.ReadLine().Replace("\r", "");
                if (response == "ready")
                {
                    _serialPort.Write(json);
                    response = _serialPort.ReadLine().Replace("\r", "");
                    if(response == "completed")
                    {
                        MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Failed to save configuration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            _connectDeviceSerialWindow.ConnectivityCheck = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to close application?",
                "Closing application",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                _connectDeviceSerialWindow.Close();
            }
        }
    }
}