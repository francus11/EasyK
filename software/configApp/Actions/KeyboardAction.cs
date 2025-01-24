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
        //TODO add modifiers to json
        public ModifierKeys Modifiers { get; private set; }
        public ClickType ClickType { get; private set; }

        public KeyboardAction(ClickType clickType, Key? key, ModifierKeys modifiers)
        {
            CombinationKey = key;
            Modifiers = modifiers;
            ClickType = clickType;
            
            List<ModifierKeys> modifierKeys = new List<ModifierKeys>();
            if ((modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                modifierKeys.Add(ModifierKeys.Control);
            }
            if ((modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                modifierKeys.Add(ModifierKeys.Shift);
            }
            if ((modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                modifierKeys.Add(ModifierKeys.Alt);
            }
            if ((modifiers & ModifierKeys.Windows) == ModifierKeys.Windows)
            {
                modifierKeys.Add(ModifierKeys.Windows);
            }
            Label = "Key: ";
            Label += string.Join(" + ", modifierKeys.Select(x => x.ToString()));
            if (CombinationKey != null && !(CombinationKey >= Key.LeftShift && CombinationKey <= Key.RightAlt))
            {
                Label += modifierKeys.Count == 0 ? CombinationKey : " + " + CombinationKey;
            }

            if (ClickType != ClickType.None)
            {
                Label += " (" + ClickTypeToString.Dictionary[ClickType] + ")";
            }
        }
    }
}
