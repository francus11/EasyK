using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Actions
{
    public class Macro : IAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Command { get; set; }
    }
}
