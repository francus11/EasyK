using System.Windows.Input;

public static class KeyToHidUsageIdMapper
{
    private static readonly Dictionary<Key, int> _keyToUsageIdMap = new()
    {
        { Key.A, 0x04 },
        { Key.B, 0x05 },
        { Key.C, 0x06 },
        { Key.D, 0x07 },
        { Key.E, 0x08 },
        { Key.F, 0x09 },
        { Key.G, 0x0A },
        { Key.H, 0x0B },
        { Key.I, 0x0C },
        { Key.J, 0x0D },
        { Key.K, 0x0E },
        { Key.L, 0x0F },
        { Key.M, 0x10 },
        { Key.N, 0x11 },
        { Key.O, 0x12 },
        { Key.P, 0x13 },
        { Key.Q, 0x14 },
        { Key.R, 0x15 },
        { Key.S, 0x16 },
        { Key.T, 0x17 },
        { Key.U, 0x18 },
        { Key.V, 0x19 },
        { Key.W, 0x1A },
        { Key.X, 0x1B },
        { Key.Y, 0x1C },
        { Key.Z, 0x1D },

        { Key.D1, 0x1E },
        { Key.D2, 0x1F },
        { Key.D3, 0x20 },
        { Key.D4, 0x21 },
        { Key.D5, 0x22 },
        { Key.D6, 0x23 },
        { Key.D7, 0x24 },
        { Key.D8, 0x25 },
        { Key.D9, 0x26 },
        { Key.D0, 0x27 },

        { Key.F1, 0x3A },
        { Key.F2, 0x3B },
        { Key.F3, 0x3C },
        { Key.F4, 0x3D },
        { Key.F5, 0x3E },
        { Key.F6, 0x3F },
        { Key.F7, 0x40 },
        { Key.F8, 0x41 },
        { Key.F9, 0x42 },
        { Key.F10, 0x43 },
        { Key.F11, 0x44 },
        { Key.F12, 0x45 },

        { Key.Enter, 0x28 },
        { Key.Escape, 0x29 },
        { Key.Back, 0x2A },
        { Key.Tab, 0x2B },
        { Key.Space, 0x2C },
        { Key.CapsLock, 0x39 },

        { Key.Up, 0x52 },
        { Key.Down, 0x51 },
        { Key.Left, 0x50 },
        { Key.Right, 0x4F }
    };

    public static int? GetUsageId(Key key)
    {
        if (_keyToUsageIdMap.TryGetValue(key, out int usageId))
        {
            return usageId;
        }
        return null; 
    }

    public static Key? GetKey(int usageId)
    {
        return _keyToUsageIdMap.FirstOrDefault(x => x.Value == usageId).Key;
    }
}