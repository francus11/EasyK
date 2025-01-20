using configApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.UI
{
    interface IInputButton
    {
        public IControl? SavedControl { get; set; }
        public bool IsChecked { get; set; }
    }
}
