using configApp.Actions;
using configApp.ConfigToolbar;
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
    public enum CaptureType
    {
        Auto,
        Manual
    }

    public partial class KeysCapture : Window
    {
        private ModifierKeys _modifiers;
        private ClickType _clickType;
        private KeyboardAction? _onLoadKeyboardAction;
        public static readonly Dictionary<Key, string> KeyDescriptions = new()
        {
            { Key.A, "A" },
            { Key.B, "B" },
            { Key.C, "C" },
            { Key.D, "D" },
            { Key.E, "E" },
            { Key.F, "F" },
            { Key.G, "G" },
            { Key.H, "H" },
            { Key.I, "I" },
            { Key.J, "J" },
            { Key.K, "K" },
            { Key.L, "L" },
            { Key.M, "M" },
            { Key.N, "N" },
            { Key.O, "O" },
            { Key.P, "P" },
            { Key.Q, "Q" },
            { Key.R, "R" },
            { Key.S, "S" },
            { Key.T, "T" },
            { Key.U, "U" },
            { Key.V, "V" },
            { Key.W, "W" },
            { Key.X, "X" },
            { Key.Y, "Y" },
            { Key.Z, "Z" },

            { Key.D1, "1" },
            { Key.D2, "2" },
            { Key.D3, "3" },
            { Key.D4, "4" },
            { Key.D5, "5" },
            { Key.D6, "6" },
            { Key.D7, "7" },
            { Key.D8, "8" },
            { Key.D9, "9" },
            { Key.D0, "0" },

            { Key.F1, "F1" },
            { Key.F2, "F2" },
            { Key.F3, "F3" },
            { Key.F4, "F4" },
            { Key.F5, "F5" },
            { Key.F6, "F6" },
            { Key.F7, "F7" },
            { Key.F8, "F8" },
            { Key.F9, "F9" },
            { Key.F10, "F10" },
            { Key.F11, "F11" },
            { Key.F12, "F12" },

            { Key.Enter, "Enter" },
            { Key.Escape, "Escape" },
            { Key.Back, "Backspace" },
            { Key.Tab, "Tab" },
            { Key.Space, "Spacebar" },

            { Key.LeftShift, "Left Shift" },
            { Key.RightShift, "Right Shift" },
            { Key.LeftCtrl, "Left Control" },
            { Key.RightCtrl, "Right Control" },
            { Key.LeftAlt, "Left Alt" },
            { Key.RightAlt, "Right Alt" },

            { Key.Up, "Arrow Up" },
            { Key.Down, "Arrow Down" },
            { Key.Left, "Arrow Left" },
            { Key.Right, "Arrow Right" },

            { Key.Insert, "Insert" },
            { Key.Delete, "Delete" },
            { Key.Home, "Home" },
            { Key.End, "End" },
            { Key.PageUp, "Page Up" },
            { Key.PageDown, "Page Down" },

            { Key.NumPad0, "Numpad 0" },
            { Key.NumPad1, "Numpad 1" },
            { Key.NumPad2, "Numpad 2" },
            { Key.NumPad3, "Numpad 3" },
            { Key.NumPad4, "Numpad 4" },
            { Key.NumPad5, "Numpad 5" },
            { Key.NumPad6, "Numpad 6" },
            { Key.NumPad7, "Numpad 7" },
            { Key.NumPad8, "Numpad 8" },
            { Key.NumPad9, "Numpad 9" },

            { Key.Add, "Numpad Plus" },
            { Key.Subtract, "Numpad Minus" },
            { Key.Multiply, "Numpad Multiply" },
            { Key.Divide, "Numpad Divide" },
            { Key.Decimal, "Numpad Decimal" },
            { Key.NumLock, "Num Lock" },

            { Key.Scroll, "Scroll Lock" },
            { Key.Pause, "Pause Key" },

            { Key.OemTilde, "~" },
            { Key.OemMinus, "-" },
            { Key.OemPlus, "=" },
            { Key.OemOpenBrackets, "[" },
            { Key.OemCloseBrackets, "]" },
            { Key.OemSemicolon, ";" },
            { Key.OemQuotes, "\'" },
            { Key.OemComma, "," },
            { Key.OemPeriod, "." },
            { Key.OemQuestion, "?" },
            { Key.OemPipe, "|" },
            { Key.OemBackslash, "\\" }
        };

        public static readonly DependencyProperty CaptureTypeProperty =
            DependencyProperty.Register(nameof(CaptureType), typeof(CaptureType), typeof(KeysCapture), new PropertyMetadata(CaptureType.Auto, OnCaptureTypeChanged));

        public static readonly DependencyProperty CapturedKeyProperty =
            DependencyProperty.Register(nameof(CapturedKey), typeof(Key), typeof(KeysCapture), new PropertyMetadata(Key.None, OnConditionChanged));

        public static readonly DependencyProperty ClickTypeProperty =
            DependencyProperty.Register(nameof(ClickType), typeof(ClickType), typeof(KeysCapture), new PropertyMetadata(ClickType.None, OnConditionChanged));

        public static readonly DependencyProperty CapturedModifiersProperty =
            DependencyProperty.Register(nameof(CapturedModifiers), typeof(ModifierKeys), typeof(KeysCapture), new PropertyMetadata(ModifierKeys.None, OnConditionChanged));
        public CaptureType CaptureType
        {
            get => (CaptureType)GetValue(CaptureTypeProperty);
            set => SetValue(CaptureTypeProperty, value);
        }

        public Key? CapturedKey 
        {
            get => (Key)GetValue(CapturedKeyProperty);
            set => SetValue(CapturedKeyProperty, value);
        }

        public ClickType ClickType 
        { 
            get { return (ClickType)GetValue(ClickTypeProperty); }
            set
            {
                SetValue(ClickTypeProperty, value);
                UpdateClickTypeRadioButtons();
            }
        }

        public ModifierKeys CapturedModifiers
        {
            get => (ModifierKeys)GetValue(CapturedModifiersProperty);
            set => SetValue(CapturedModifiersProperty, value);
        }

        public KeyboardAction? OnLoadKeyboardAction 
        { 
            private get { return _onLoadKeyboardAction; } 
            set
            {
                _onLoadKeyboardAction = value;
                if (_onLoadKeyboardAction != null)
                {
                    CapturedKey = _onLoadKeyboardAction.CombinationKey;
                    CapturedModifiers = _onLoadKeyboardAction.Modifiers;
                    ClickType = _onLoadKeyboardAction.ClickType;
                }
            }
        }

        public KeyboardAction? CreatedKeyboardAction { get; private set; }

        public KeysCapture()
        {
            InitializeComponent();
            ChangeCaptureTab();
            InitializeKeysComboBox();

            OnConditionChanged(this, new DependencyPropertyChangedEventArgs());
        }

        private void InitializeKeysComboBox()
        {
            KeysComboBox.ItemsSource = KeyDescriptions;
            KeysComboBox.DisplayMemberPath = "Value";
            KeysComboBox.SelectedValuePath = "Key";
        }

        private void UpdateClickTypeRadioButtons()
        {
            switch (ClickType)
            {
                case ClickType.Press:
                    PressRadioButton.IsChecked = true;
                    break;
                case ClickType.Release:
                    ReleaseRadioButton.IsChecked = true;
                    break;
                case ClickType.Click:
                    ClickRadioButton.IsChecked = true;
                    break;
                case ClickType.None:
                default:
                    PressRadioButton.IsChecked = false;
                    ReleaseRadioButton.IsChecked = false;
                    ClickRadioButton.IsChecked = false;
                    break;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            List<ModifierKeys> modifiersList = [];
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                modifiersList.Add(ModifierKeys.Control);
                ControlCheckBox.IsChecked = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
            {
                modifiersList.Add(ModifierKeys.Alt);
                AltCheckBox.IsChecked = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                modifiersList.Add(ModifierKeys.Shift);
                ShiftCheckBox.IsChecked = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Windows) != 0)
            {
                modifiersList.Add(ModifierKeys.Windows);
                WindowsCheckBox.IsChecked = true;
            }

            CapturedModifiers = Keyboard.Modifiers;

            SetModifierCheckBoxes();

            if ((e.Key >= Key.LeftShift && e.Key <= Key.RightAlt) || e.Key == Key.System)
            {
                CapturedKey = Key.None;
            }
            else
            {
                CapturedKey = e.Key;
            }

            ClickType = ClickType.Click;

            // TODO : Solve problem with not capturing Alt + key combinations
            // TODO : Solve problem with not capturing WIN + key combinations
        }

        private void SetModifierCheckBoxes()
        {
            ControlCheckBox.IsChecked = CapturedModifiers.HasFlag(ModifierKeys.Control);
            AltCheckBox.IsChecked = CapturedModifiers.HasFlag(ModifierKeys.Alt);
            ShiftCheckBox.IsChecked = CapturedModifiers.HasFlag(ModifierKeys.Shift);
            WindowsCheckBox.IsChecked = CapturedModifiers.HasFlag(ModifierKeys.Windows);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CreatedKeyboardAction = new KeyboardAction(ClickType, CapturedKey, CapturedModifiers);

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag)
            {
                CaptureType = tag switch
                {
                    "Auto" => CaptureType.Auto,
                    "Manual" => CaptureType.Manual,
                    _ => CaptureType
                };
            }
        }

        private void ChangeCaptureTab()
        {

            if (CaptureType == CaptureType.Auto)
            {
                AutoCaptureTab.Visibility = Visibility.Visible;
                ManualCaptureTab.Visibility = Visibility.Hidden;
                this.PreviewKeyDown += Window_PreviewKeyDown;
            }
            else if (CaptureType == CaptureType.Manual)
            {
                AutoCaptureTab.Visibility = Visibility.Hidden;
                ManualCaptureTab.Visibility = Visibility.Visible;
                this.PreviewKeyDown -= Window_PreviewKeyDown;
            }
        }

        private static void OnCaptureTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is KeysCapture toolbar)
            {
                toolbar.ChangeCaptureTab();
            }
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

        private void ControlCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if ((string)checkBox.Tag == "Control")
                    CapturedModifiers |= ModifierKeys.Control;
                else if ((string)checkBox.Tag == "Alt")
                    CapturedModifiers |= ModifierKeys.Alt;
                else if ((string)checkBox.Tag == "Shift")
                    CapturedModifiers |= ModifierKeys.Shift;
                else if ((string)checkBox.Tag == "Windows")
                    CapturedModifiers |= ModifierKeys.Windows;
            }

        }

        private void ControlCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if ((string)checkBox.Tag == "Control")
                    CapturedModifiers &= ~ModifierKeys.Control;
                else if ((string)checkBox.Tag == "Alt")
                    CapturedModifiers &= ~ModifierKeys.Alt;
                else if ((string)checkBox.Tag == "Shift")
                    CapturedModifiers &= ~ModifierKeys.Shift;
                else if ((string)checkBox.Tag == "Windows")
                    CapturedModifiers &= ~ModifierKeys.Windows;
            }
        }

        private static void OnConditionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is KeysCapture window)
            {

                window.OKButton.IsEnabled = window.ClickType != ClickType.None && (window.CapturedKey != Key.None || window.CapturedModifiers != ModifierKeys.None);
                List<ModifierKeys> modifiersList = [];
                if ((window.CapturedModifiers & ModifierKeys.Control) != 0)
                {
                    modifiersList.Add(ModifierKeys.Control);
                }
                if ((window.CapturedModifiers & ModifierKeys.Alt) != 0)
                {
                    modifiersList.Add(ModifierKeys.Alt);
                }
                if ((window.CapturedModifiers & ModifierKeys.Shift) != 0)
                {
                    modifiersList.Add(ModifierKeys.Shift);
                }
                if ((window.CapturedModifiers & ModifierKeys.Windows) != 0)
                {
                    modifiersList.Add(ModifierKeys.Windows);
                }


                window.KeyCombinationText.Text = string.Join(" + ", modifiersList.Select(x => x.ToString()));
                if ((window.CapturedKey >= Key.LeftShift && window.CapturedKey <= Key.RightAlt) || window.CapturedKey == Key.None)
                {
                }
                else
                {
                    window.KeyCombinationText.Text += modifiersList.Count == 0 ? window.CapturedKey : " + " + window.CapturedKey;

                }
            }
        }
    }
}

