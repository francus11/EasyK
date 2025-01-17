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
        public IAction? LeftAction { get; set; }
        public IAction? RightAction { get; set; }

        public EncoderControl() { }
        public EncoderControl(EncoderControl encoder)
        {
            Id = encoder.Id;
            LeftAction = encoder.LeftAction;
            RightAction = encoder.RightAction;
        }
    }
}
