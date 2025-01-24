﻿using System.Windows.Input;

public static class KeyToHidUsageIdMapper
{
    public static readonly Dictionary<Key, int> KeyToUsageIdMap = new()
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
        { Key.Right, 0x4F },

        { Key.LeftShift, 0xE1 },
        { Key.RightShift, 0xE5 },
        { Key.LeftCtrl, 0xE0 },
        { Key.RightCtrl, 0xE4 },
        { Key.LeftAlt, 0xE2 },
        { Key.RightAlt, 0xE6},
        { Key.LWin, 0xE3    },
        { Key.RWin, 0xE7 },

        { Key.Insert, 0x49 },
        { Key.Delete, 0x4C },
        { Key.Home, 0x4A },
        { Key.End, 0x4D },
        { Key.PageUp, 0x4B },
        { Key.PageDown, 0x4E },

        { Key.NumPad0, 0x62 },
        { Key.NumPad1, 0x59 },
        { Key.NumPad2, 0x5A },
        { Key.NumPad3, 0x5B },
        { Key.NumPad4, 0x5C },
        { Key.NumPad5, 0x5D },
        { Key.NumPad6, 0x5E },
        { Key.NumPad7, 0x5F },
        { Key.NumPad8, 0x60 },
        { Key.NumPad9, 0x61 },

        { Key.Add, 0x57 },
        { Key.Subtract, 0x56 },
        { Key.Multiply, 0x55 },
        { Key.Divide, 0x54 },
        { Key.Decimal, 0x63 },
        { Key.NumLock, 0x53 },

        { Key.Scroll, 0x47 },
        { Key.Pause, 0x48 },

        { Key.OemTilde, 0x35 },
        { Key.OemMinus, 0x2D },
        { Key.OemPlus, 0x2E },
        { Key.OemOpenBrackets, 0x2F },
        { Key.OemCloseBrackets, 0x30 },
        { Key.OemSemicolon, 0x33 },
        { Key.OemQuotes, 0x34 },
        { Key.OemComma, 0x36 },
        { Key.OemPeriod, 0x37 },
        { Key.OemQuestion, 0x38 },
        { Key.OemPipe, 0x64 },
        { Key.OemBackslash, 0x64 },
    };

    public static int? GetUsageId(Key key)
    {
        if (KeyToUsageIdMap.TryGetValue(key, out int usageId))
        {
            return usageId;
        }
        return null; 
    }

    public static Key? GetKey(int usageId)
    {
        return KeyToUsageIdMap.FirstOrDefault(x => x.Value == usageId).Key;
    }
}