using configApp.Actions;
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
    public class KeyButton : Button
    {
        public Macro? Macro { get; private set; }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(KeyButton),
                new PropertyMetadata(false, OnIsCheckedChanged));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Zdarzenia Checked i Unchecked
        public event RoutedEventHandler Checked;
        public event RoutedEventHandler Unchecked;

        public KeyButton()
        {
            // Zdarzenie kliknięcia zmienia stan IsChecked
            this.Click += (s, e) =>
            {
                // Zmiana stanu IsChecked
                IsChecked = !IsChecked;
            };
        }

        // Callback, który obsługuje zmianę IsChecked
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as KeyButton;
            if (button == null) return;

            // Sprawdzanie, czy stan się zmienił na Checked lub Unchecked
            if ((bool)e.NewValue)
            {
                button.OnChecked();
            }
            else
            {
                button.OnUnchecked();
            }
        }

        // Metoda do obsługi zdarzenia Checked
        private void OnChecked()
        {
            Checked?.Invoke(this, new RoutedEventArgs());
        }

        // Metoda do obsługi zdarzenia Unchecked
        private void OnUnchecked()
        {
            Unchecked?.Invoke(this, new RoutedEventArgs());
        }

    }
}
