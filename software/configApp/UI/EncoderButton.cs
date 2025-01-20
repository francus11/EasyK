using configApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace configApp.UI
{
    public class EncoderButton : Button, IInputButton
    {
        public IControl? SavedControl { get; set; }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(EncoderButton),
                new PropertyMetadata(false, OnIsCheckedChanged));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public event RoutedEventHandler Checked;
        public event RoutedEventHandler Unchecked;

        public EncoderButton()
        {
            SavedControl = new EncoderControl();

            // Domyślne stylizowanie przycisku jako okrągłego
            this.Width = 50;
            this.Height = 50;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;

            // Ustawienie eliptycznego kształtu
            this.Style = CreateRoundButtonStyle();

            this.Click += (s, e) =>
            {
                IsChecked = !IsChecked;
            };
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as EncoderButton;
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

        /// <summary>
        /// Tworzy styl dla przycisku, który czyni go okrągłym.
        /// </summary>
        /// <returns>Styl przycisku.</returns>
        private Style CreateRoundButtonStyle()
        {
            var style = new Style(typeof(Button));

            style.Setters.Add(new Setter(WidthProperty, 50.0));
            style.Setters.Add(new Setter(HeightProperty, 50.0));
            style.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(2)));
            style.Setters.Add(new Setter(BorderBrushProperty, Brushes.Black));
            style.Setters.Add(new Setter(BackgroundProperty, Brushes.LightGray));
            style.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
            style.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
            style.Setters.Add(new Setter(MarginProperty, new Thickness(5)));

            // Ustawienie eliptycznego kształtu poprzez modyfikację właściwości "CornerRadius"
            var template = new ControlTemplate(typeof(Button));
            var ellipse = new FrameworkElementFactory(typeof(Ellipse));
            ellipse.SetValue(Shape.FillProperty, new TemplateBindingExtension(BackgroundProperty));
            ellipse.SetValue(Shape.StrokeProperty, new TemplateBindingExtension(BorderBrushProperty));
            ellipse.SetValue(Shape.StrokeThicknessProperty, new TemplateBindingExtension(BorderThicknessProperty));

            var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

            var grid = new FrameworkElementFactory(typeof(Grid));
            grid.AppendChild(ellipse);
            grid.AppendChild(contentPresenter);

            template.VisualTree = grid;
            style.Setters.Add(new Setter(TemplateProperty, template));

            return style;
        }
    }
}

