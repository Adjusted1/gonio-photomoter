using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer
{
    /// <summary>
    /// use extensions for start/stop values
    /// </summary>
    public enum VerticalSymmetryEnum : int
    {
        Full = 3,      // 0-360
        TopOnly = 2,   // 180-360
        BottomOnly = 1 // 0-180
    }

    public static class VerticalSymmetryEnumExtension
    {
        public static int StartAngle(this VerticalSymmetryEnum symmetry)
        {
            switch (symmetry)
            {
                case VerticalSymmetryEnum.BottomOnly:
                    return 0;
                case VerticalSymmetryEnum.TopOnly:
                    return 90;
                case VerticalSymmetryEnum.Full:
                    return 0;
                default:
                    return 0;
            }
        }

        public static int StopAngle(this VerticalSymmetryEnum symmetry)
        {
            switch (symmetry)
            {
                case VerticalSymmetryEnum.BottomOnly:
                    return 90;
                case VerticalSymmetryEnum.TopOnly:
                    return 180;
                case VerticalSymmetryEnum.Full:
                    return 180;
                default:
                    return 180;
            }
        }
    }

    /// <summary>
    /// use int value for horizontal stop angle
    /// </summary>
    public enum HorizontalSymmetryEnum : int
    {
        Full = 360,
        Half = 180,
        Quarter = 90,
        Single = 0
    }
}
