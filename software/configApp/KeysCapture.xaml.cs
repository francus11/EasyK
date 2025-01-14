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
    public partial class KeysCapture : Window
    {
        private List<ModifierKeys> _modifiersList = new List<ModifierKeys>();

        public string CapturedKeyCombination { get; private set; } = string.Empty;
        public Key? CapturedKey { get; private set; } = null;
        public List<ModifierKeys> CapturedModifiers { get; private set; } = new List<ModifierKeys>();
        public KeyboardAction? OnLoadKeyboardAction { get; set; }
        public KeyboardAction? CreatedKeyboardAction { get; private set; }

        public KeysCapture()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                _modifiersList.Add(ModifierKeys.Control);

            }
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
            {
                _modifiersList.Add(ModifierKeys.Alt);
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                _modifiersList.Add(ModifierKeys.Shift);
            }
            if ((Keyboard.Modifiers & ModifierKeys.Windows) != 0)
            {
                _modifiersList.Add(ModifierKeys.Windows);
            }

            KeyCombinationText.Text = string.Join(" + ", _modifiersList.Select(x => x.ToString()));

            if (e.Key >= Key.LeftShift && e.Key <= Key.RightAlt)
            {
                CapturedKey = null;
            }
            else
            {
                CapturedKey = e.Key;
                KeyCombinationText.Text += _modifiersList.Count == 0 ? CapturedKey : " + " + CapturedKey;

            }

            CapturedModifiers = new List<ModifierKeys>(_modifiersList);
            _modifiersList.Clear();

            // TODO : Solve problem with not capturing Alt + key combinations
            // TODO : Solve problem with not capturing WIN + key combinations
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //CapturedKeyCombination = string.Join(" + ", CapturedModifiers.Select(x => x.ToString()));

            //if (CapturedKey != null && !(CapturedKey >= Key.LeftShift && CapturedKey <= Key.RightAlt))
            //{
            //    CapturedKeyCombination += CapturedModifiers.Count == 0 ? CapturedKey : " + " + CapturedKey;
            //}

            CreatedKeyboardAction = new KeyboardAction(ClickType.Press, CapturedKey, CapturedModifiers);

            CapturedKeyCombination = CreatedKeyboardAction.Label;

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

