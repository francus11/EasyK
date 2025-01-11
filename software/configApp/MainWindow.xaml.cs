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

        private int counter = 0;
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
                /*keyButton. = !keyButton.IsPressed;*/
                /*keyButton.Toggle();*/

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
        

        private void UpdateCanvas(Macro? macro)
        {
            SelectedElementConfig.Children.Clear();
            if (macro != null)
            {
                var textBlock = new TextBlock
                {
                    Text = $"Configuring: {macro.Name}",
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