using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goniometer_Controller.Motors
{
    /// <summary>
    /// enum for specifying move speed, int values for slowing factor
    /// </summary>
    public enum MotionSpeed : int
    {
        Normal = 1,
        Slow = 2,
        Slowest = 4
    }
}
