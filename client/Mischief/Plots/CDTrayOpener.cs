using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mischief.Plots
{
    class CDTrayOpener : IPlot
    {
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern Int32 mciSendString(string lpszCommand, StringBuilder lpszReturnString, Int32 cchReturn, IntPtr hwndCallback);

        public void Plot()
        {
            // open the cd tray
            mciSendString("set CDAudio door open", null, 127, IntPtr.Zero);
        }
    }
}
