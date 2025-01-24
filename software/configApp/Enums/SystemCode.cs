using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configApp.Enums
{
    public enum SystemCode 
    {
        None = 0x00,
        Power = 0x30,
        Sleep = 0x32,
        VolumeUp = 0xE9,
        VolumeDown = 0xEA,
        VolumeMute = 0xE2,
        BrightnessUp = 0x6F,
        BrightnessDown = 0x70,
        MediaPlayPause = 0xCD,
        MediaStop = 0xB7,
        MediaNext = 0xB5,
        MediaPrevious = 0xB6,
        MediaPause = 0xB0,

    }
}
