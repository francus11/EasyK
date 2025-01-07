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

using System.Windows;
using System.Windows.Input;

namespace configApp
{
    public partial class KeysCapture : Window
    {
        private ModifierKeys _modifiers = ModifierKeys.None;

        public KeysCapture()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                _modifiers |= ModifierKeys.Control;
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
                _modifiers |= ModifierKeys.Alt;
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                _modifiers |= ModifierKeys.Shift;
            if ((Keyboard.Modifiers & ModifierKeys.Windows) != 0)
                _modifiers |= ModifierKeys.Windows;

            string modifiersText = _modifiers != ModifierKeys.None ? _modifiers.ToString() + " + " : string.Empty;
            KeyCombinationText.Text = modifiersText + e.Key;

            _modifiers = ModifierKeys.None;
        }
    }
}

