using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Mischief.Plots
{
    class HauntedCursor : IPlot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SetCursorPos(int x, int y);

        public void Plot()
        {
            // move the mouse cursor (pointer) across the current screen from the center
            var screen = System.Windows.Forms.Screen.FromPoint(Cursor.Position).WorkingArea;
            Point center = new Point(screen.Left + screen.Width / 2, screen.Top + screen.Height / 2);

            for (int i = 0; i < 300; i++)
            {
                SetCursorPos(center.X + i, center.Y);
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
