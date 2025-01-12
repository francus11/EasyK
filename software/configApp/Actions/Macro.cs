using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Actions
{
    public class Macro : IAction
    {
        public List<IAction> ActionList { get; set; }

        public string Label { get; private set; }

        public Macro(List<IAction> actions)
        {
            ActionList = actions;
            Label = "Macro";
        }
    }
}
