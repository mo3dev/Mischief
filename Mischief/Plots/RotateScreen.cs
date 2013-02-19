using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mischief.Plots
{
    class RotateScreen
    {
        public void Plot()
        {
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
        }
    }
}
