using configApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace configApp.ConfigToolbar
{
    /// <summary>
    /// Interaction logic for KeyAdvancedToolbar.xaml
    /// </summary>
    public partial class KeyAdvancedToolbar : UserControl
    {
        public KeyAdvancedToolbar()
        {
            InitializeComponent();
        }

        private void AddActionButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO : Add logic to select type of action
            KeysCapture keysCaptureWindow = new KeysCapture();
            bool? result = keysCaptureWindow.ShowDialog();
            if (result == true)
            {
                ActionsStackPanel.Children.Add(new ActionStackPanelItem
                {
                    LabelContent = keysCaptureWindow.CapturedKeyCombination,
                    Action = keysCaptureWindow.CreatedKeyboardAction
                });
            }
        }

        //TODO Konkretne toolbary powinny zwracać gotowe obiekty typu IControl
    }
}
