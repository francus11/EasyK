using configApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Actions
{
    public class SystemAction : IAction
    {
        public string Label
        {
            get
            {
                return "System: " + SystemCode.ToString() + "(" + ClickType.ToString() + ")";
            }
        }

        public SystemCode SystemCode { get; set; }

        public ClickType ClickType { get; set; }
        public SystemAction(SystemCode systemCode, ClickType clickType)
        {
            SystemCode = systemCode;
            ClickType = clickType;
        }
    }
}
