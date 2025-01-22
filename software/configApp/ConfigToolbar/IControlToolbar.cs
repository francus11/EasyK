using configApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.ConfigToolbar
{
    public interface IControlToolbar
    {
        //TODO Add event for saving
        public IControl NewControl { get; }

        public IControl OldControl { set; }
    }
}
