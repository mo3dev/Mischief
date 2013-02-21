using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mischief.Plots
{
    class AlertMaddness : IPlot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MessageBox(IntPtr hWnd, String lpText, String lpCaption, uint uType);

        public void Plot()
        {
            // Display MessageBox
            ShowAlert("You have been PWNED.!\nAll your files are belong to us", "H4x0r'ed.. HAhA", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private static int ShowAlert(string text, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return MessageBox(new IntPtr(0), text, caption, (uint)button + (uint)icon + (uint)defaultButton + (uint)options);
        }
    }
}
