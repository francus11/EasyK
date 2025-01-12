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
        private IAction _action;
        public string LabelContent
        {
            get => ContentLabel.Content.ToString();
            set => ContentLabel.Content = value;
        }

        public ActionStackPanelItem()
        {
            InitializeComponent();
        }
    }
}
