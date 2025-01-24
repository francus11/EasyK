using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Enums
{
    public enum ClickType
    {
        None,
        Press,
        Release,
        Click,
        Hold
    }

    static class ClickTypeToString
    {
        static public Dictionary<ClickType, string> Dictionary = new Dictionary<ClickType, string>
        {
            { ClickType.None, "None" },
            { ClickType.Press, "Press" },
            { ClickType.Release, "Release" },
            { ClickType.Click, "Click" },
            { ClickType.Hold, "Hold" }
        };
    }
}
