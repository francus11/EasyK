using configApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Controls
{
    public class EncoderControl : IControl
    {
        public int Id { get; set; }
        public IAction? ActionLeft { get; set; }
        public IAction? ActionRight { get; set; }
        public IAction? ActionButton { get; set; }

        public EncoderControl() { }
        public EncoderControl(EncoderControl encoder)
        {
            Id = encoder.Id;
            ActionLeft = encoder.ActionLeft;
            ActionRight = encoder.ActionRight;
            ActionButton = encoder.ActionButton;
        }
    }
}
