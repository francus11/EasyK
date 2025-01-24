using configApp.Controls;
using configApp.Enums;
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
    /// Interaction logic for BasicToolbar.xaml
    /// </summary>
    public partial class BasicToolbar : UserControl
    {
        public static readonly DependencyProperty ToolbarTypeProperty =
        DependencyProperty.Register(nameof(ToolbarType), typeof(ToolbarType), typeof(BasicToolbar), new PropertyMetadata(ToolbarType.Basic, OnToolbarTypeChanged));

        public ToolbarType ToolbarType
        {
            get => (ToolbarType)GetValue(ToolbarTypeProperty);
            set => SetValue(ToolbarTypeProperty, value);
        }
        
        public event RoutedEventHandler? SaveClicked;

        private IControl _control;
        private IControlToolbar _controlToolbar;
        public IControl Control
        {
            get => _control;
            set
            {
                _control = value;
                UpdateVisuals();
            }
        }

        public BasicToolbar()
        {
            InitializeComponent();
            ToolbarType = ToolbarType.Advanced;
            UpdateVisuals();
        }

        private void ToolbarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string toolbarType = button.Tag as string;
                if (toolbarType == "Basic")
                    ToolbarType = ToolbarType.Basic;
                else if (toolbarType == "Advanced")
                    ToolbarType = ToolbarType.Advanced;
            }
        }

        private void UpdateVisuals()
        {
            ContentGrid.Children.Clear();

            if (ToolbarType == ToolbarType.Basic)
            {
                if (Control is ButtonControl buttonControl)
                {
                    //TODO Add basic toolbar content
                }
                else if  (Control is EncoderControl encoderControl)
                {

                }
            }
            else if (ToolbarType == ToolbarType.Advanced)
            {
                if (Control is ButtonControl buttonControl)
                { 
                    KeyAdvancedToolbar keyAdvancedToolbar = new KeyAdvancedToolbar();
                    _controlToolbar = keyAdvancedToolbar;
                    _controlToolbar.OldControl = buttonControl;
                    ContentGrid.Children.Add(keyAdvancedToolbar);
                }
                else if (Control is EncoderControl encoderControl)
                {
                    EncoderAdvancedToolbar encoderAdvancedToolbar = new EncoderAdvancedToolbar();
                    _controlToolbar = encoderAdvancedToolbar;
                    _controlToolbar.OldControl = encoderControl;

                    ContentGrid.Children.Add(encoderAdvancedToolbar);
                }
            }
        }
        private static void OnToolbarTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is BasicToolbar toolbar)
            {
                toolbar.UpdateVisuals();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Control = _controlToolbar.NewControl;
            SaveClicked?.Invoke(this, e);
        }
    }
}
