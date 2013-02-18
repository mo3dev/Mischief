using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Mischief.Plots
{
    class MouseBtnSwitcher : IPlot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SwapMouseButton(Int32 buttonSwap);

        public void Plot()
        {
            // swap mouse buttons (0 = normal, 1 = swap)
            SwapMouseButton(1);
        }
    }
}
