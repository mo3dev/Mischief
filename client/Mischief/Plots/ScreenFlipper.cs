using System;
using System.Runtime.InteropServices;
using Mischief.Utils;

namespace Mischief.Plots
{
    class ScreenFlipper : IPlot
    {
        public void Plot()
        {
            // get the current devmode (display settings), change the orientation, then persist the devmode
            DEVMODE dm = DisplaySettingsUtil.CreateDevmode();
            DisplaySettingsUtil.EnumDisplaySettings(null, -1, ref dm);
            dm.dmDisplayOrientation = 2; // 2 = 180 (flipped)

            // persist (switch to new devmode)
            int result = DisplaySettingsUtil.ChangeDisplaySettings(ref dm, 0);
        }
    }
}
