using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Models
{
    public class MeasurementKeys
    {
        /* Don't use commas in these strings as they are used in CSV Files
         * */

        public const string ColorX = "Color - X";
        public const string ColorY = "Color - Y";
        public const string ColorZ = "Color - Z";
        public const string ColorU = "Color - U'";
        public const string ColorV = "Color - V'";

        public const string ColorTemp = "Color - CorrelatedTemp";
        public const string ColorDiff = "Color - DeltaUV";

        public const string Illuminance = "Illuminance - Ev (fcd)";
        public const string LuminousIntensity = "LuminousIntensity - cd";
    }
}
