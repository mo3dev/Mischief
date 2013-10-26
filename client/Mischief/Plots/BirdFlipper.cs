using System;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

namespace Mischief.Plots
{
    class BirdFlipper : IPlot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetSystemCursor(IntPtr hcur, uint id);

        public void Plot()
        {
            // download the cursor file
            string fileName = Path.Combine(Path.GetTempPath(), "cage-cursor.cur");
            DownloadImage(fileName);

            // load & set
            IntPtr cursor = LoadImage(IntPtr.Zero, fileName, 2, 0, 0, 0x00000010);
            SetSystemCursor(cursor, 32512);
        }

        private static void DownloadImage(string fileName)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("http://www.seomofo.com/img/cursors/aero-middle-finger-2.cur", fileName);
            }
        }
    }
}
