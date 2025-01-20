using configApp.Actions;
using configApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace configApp.UI
{
    public class KeyButton : Button, IInputButton
    {
        public IControl? SavedControl { get; set; }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(KeyButton),
                new PropertyMetadata(false, OnIsCheckedChanged));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public event RoutedEventHandler Checked;
        public event RoutedEventHandler Unchecked;

        public KeyButton()
        {
            SavedControl = new ButtonControl();
            this.Click += (s, e) =>
            {
                IsChecked = !IsChecked;
            };
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as KeyButton;
            if (button == null) return;

            if ((bool)e.NewValue)
            {
                button.OnChecked();
            }
            else
            {
                button.OnUnchecked();
            }
        }

        private void OnChecked()
        {
            Checked?.Invoke(this, new RoutedEventArgs());
        }

        private void OnUnchecked()
        {
            Unchecked?.Invoke(this, new RoutedEventArgs());
        }

    }
}
