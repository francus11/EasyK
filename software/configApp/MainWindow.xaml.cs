using configApp.Actions;
using configApp.UI;
using System.Text;
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
        private KeyButton? _activeButton = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
            KeysCapture newWindow = new KeysCapture();

            newWindow.ShowDialog();
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
                    UpdateCanvas(keyButton.Macro);
                    _activeButton = keyButton;
                }
            }
        }

        private void KeyButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is KeyButton keyButton)
            {
                if (_activeButton != null)
                {
                    _activeButton.IsChecked = false;
                }
                _activeButton = keyButton;
                Console.WriteLine($"Przycisk {keyButton.Content} włączony.");
                UpdateCanvas(keyButton.Macro);
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

        private void AddTextBoxButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO : Add logic to select type of action

            KeysCapture keysCaptureWindow = new KeysCapture();
            bool? result = keysCaptureWindow.ShowDialog();
            if (result == true)
            {
                TextBoxStackPanel.Children.Add(new ActionStackPanelItem
                {
                    LabelContent = keysCaptureWindow.CapturedKeyCombination
                });
            }
        }

        private void UpdateCanvas(MacroAction? macro)
        {
            SelectedElementConfig.Children.Clear();
            if (macro != null)
            {
                var textBlock = new TextBlock
                {
                    Text = $"Configuring:",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                SelectedElementConfig.Children.Add(textBlock);
            }
            NotSelectedElementConfig.Visibility = Visibility.Hidden;
            SelectedElementConfig.Visibility = Visibility.Visible;
        }

        private void ResetCanvas()
        {
            NotSelectedElementConfig.Visibility = Visibility.Visible;
            SelectedElementConfig.Visibility = Visibility.Hidden;
        }
    }
}