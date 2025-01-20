using configApp.Actions;
using configApp.ConfigToolbar;
using configApp.Controls;
using configApp.JsonConverters;
using configApp.UI;
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
            WriteIndented = true
        };

        private IInputButton? _activeButton = null;

        public string json = "";

        public MainWindow()
        {
            InitializeComponent();
            options.Converters.Add(new IControlListJsonConverter());
            options.Converters.Add(new MacroActionJsonConverter());
            options.Converters.Add(new KeyboardActionJsonConverter());
            options.Converters.Add(new ButtonControlJsonConverter());
            options.Converters.Add(new DelayActionJsonConverter());
            options.Converters.Add(new IActionJsonConverter());
        }

        private void OpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
            
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
                Control = _activeButton.SavedControl
            };
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
    }
}