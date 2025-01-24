using configApp.Actions;
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
using System.Windows.Shapes;

namespace configApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SystemWindow : Window
    {
        private SystemAction _action;
        private Dictionary<SystemCode, string> SystemDescription = new Dictionary<SystemCode, string>
        {
            { SystemCode.None, "" },
            { SystemCode.Power, "Power" },
            { SystemCode.Sleep, "Sleep" },
            { SystemCode.VolumeUp, "Volume Up" },
            { SystemCode.VolumeDown, "Volume Down" },
            { SystemCode.VolumeMute, "Volume Mute" },
            { SystemCode.BrightnessUp, "Brightness Up" },
            { SystemCode.BrightnessDown, "Brightness Down" },
            { SystemCode.MediaPlayPause, "Media Play/Pause" },
            { SystemCode.MediaStop, "Media Stop" },
            { SystemCode.MediaNext, "Media Next" },
            { SystemCode.MediaPrevious, "Media Previous" },
            { SystemCode.MediaPause, "Media Pause" }
        };

        public static readonly DependencyProperty SystemCodeProperty =
            DependencyProperty.Register(nameof(SystemCode), typeof(SystemCode), typeof(SystemWindow), new PropertyMetadata(SystemCode.None, OnConditionChanged));

        public SystemCode SystemCode
        {
            get => (SystemCode)GetValue(SystemCodeProperty);
            set => SetValue(SystemCodeProperty, value);
        }

        public static readonly DependencyProperty ClickTypeProperty =
            DependencyProperty.Register(nameof(ClickType), typeof(ClickType), typeof(SystemWindow), new PropertyMetadata(ClickType.None, OnConditionChanged));

        public ClickType ClickType
        {
            get => (ClickType)GetValue(ClickTypeProperty);
            set => SetValue(ClickTypeProperty, value);
        }

        public SystemAction Action
        {
            get => _action;
            set
            {
                _action = value;
                SystemCode = _action.SystemCode;
                ClickType = _action.ClickType;
            }
        }

        public SystemWindow()
        {
            InitializeComponent();
            InitializeKeysComboBox();
        }

        private void InitializeKeysComboBox()
        {
            KeysComboBox.ItemsSource = SystemDescription;
            KeysComboBox.DisplayMemberPath = "Value";
            KeysComboBox.SelectedValuePath = "Key";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (PressRadioButton.IsChecked == true)
                ClickType = ClickType.Press;
            else if (ReleaseRadioButton.IsChecked == true)
                ClickType = ClickType.Release;
            else if (ClickRadioButton.IsChecked == true)
                ClickType = ClickType.Click;
        }

        private static void OnConditionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SystemWindow window)
            {
                window.OKButton.IsEnabled = window.ClickType != ClickType.None && window.SystemCode != SystemCode.None;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Action = new SystemAction(SystemCode, ClickType);

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }
    }
}
