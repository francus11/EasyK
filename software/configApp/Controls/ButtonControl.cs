using configApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Controls
{
    public class ButtonControl : IControl
    {
        public int Id { get; set; }
        public IAction? PressAction { get; set; }

        public ButtonControl() { }
        public ButtonControl(ButtonControl button)
        {
            Id = button.Id;
            PressAction = button.PressAction;
        }
    }
}
