using configApp.Actions;
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

namespace configApp.UI
{
    /// <summary>
    /// Interaction logic for ActionStackPanelItem.xaml
    /// </summary>
    public partial class ActionStackPanelItem : UserControl
    {
        public RoutedEventHandler? RemoveClicked { get; set; }
        public RoutedEventHandler? EditClicked { get; set; }
        public IAction Action { get; set; }
        public string LabelContent
        {
            get => ContentLabel.Content.ToString();
            set => ContentLabel.Content = value;
        }

        public ActionStackPanelItem()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveClicked?.Invoke(this, e);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditClicked?.Invoke(this, e);
        }
    }
}
