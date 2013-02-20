using System;
using System.Runtime.InteropServices;

namespace Mischief.Plots
{
    class WorkstationLocker : IPlot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool LockWorkStation();

        public void Plot()
        {
            // lock the computer
            LockWorkStation();
        }
    }
}
