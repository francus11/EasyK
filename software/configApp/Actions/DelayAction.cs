using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Actions
{
    public class DelayAction : IAction
    {
        public int Delay { get; private set; }
        public string Label { get; private set; };

        public DelayAction(int delay)
        {
            Delay = delay;
            Label = "Delay " + delay + "ms";
        }
    }
}
