using configApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace configApp.Actions
{
    public class KeyboardAction : IAction
    {
        public string Label { get; private set; }
        public Key? CombinationKey { get; private set; }
        public List<ModifierKeys> Modifiers { get; private set; }
        public ClickType ClickType { get; private set; }

        public KeyboardAction(ClickType clickType, Key? key, List<ModifierKeys> modifiers)
        {
            CombinationKey = key;
            Modifiers = modifiers;
            ClickType = clickType;

            Label = string.Join(" + ", Modifiers.Select(x => x.ToString()));
            if (CombinationKey != null && !(CombinationKey >= Key.LeftShift && CombinationKey <= Key.RightAlt))
            {
                Label += Modifiers.Count == 0 ? CombinationKey : " + " + CombinationKey;
            }
        }
    }
}
