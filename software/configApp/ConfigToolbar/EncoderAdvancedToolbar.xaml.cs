using configApp.Enums;
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
    /// Interaction logic for EncoderAdvancedToolbar.xaml
    /// </summary>

    public enum EncoderInputType
    {
        Left,
        Right,
        Button
    }

    public partial class EncoderAdvancedToolbar : UserControl
    {
        public static readonly DependencyProperty EncoderInputTypeProperty =
        DependencyProperty.Register(nameof(EncoderInputType), typeof(EncoderInputType), typeof(EncoderAdvancedToolbar), new PropertyMetadata(EncoderInputType.Left, OnToolbarTypeChanged));

        public EncoderInputType EncoderInputType
        {
            get => (EncoderInputType)GetValue(EncoderInputTypeProperty);
            set => SetValue(EncoderInputTypeProperty, value);
        }
        public EncoderAdvancedToolbar()
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

        private void EncoderToolbarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string toolbarType = button.Tag as string;
                if (toolbarType == "Left")
                    EncoderInputType = EncoderInputType.Left;
                else if (toolbarType == "Right")
                    EncoderInputType = EncoderInputType.Right;
                else if (toolbarType == "Button")
                    EncoderInputType = EncoderInputType.Button;
            }
        }

        private static void OnToolbarTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is EncoderAdvancedToolbar toolbar)
            {
                toolbar.UpdateVisuals();
            }
        }

        private void UpdateVisuals()
        {

        }
    }
}
