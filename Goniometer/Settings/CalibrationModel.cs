using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Goniometer.Settings
{
    public static class CalibrationModel
    {
        static CalibrationModel()
        {
            try
            {
                CalibrationModel.KCal     = Double.Parse(ConfigurationManager.AppSettings["default.correction.calibration"]);
                CalibrationModel.KTheta   = Double.Parse(ConfigurationManager.AppSettings["default.correction.theta"]);
                CalibrationModel.Distance = Double.Parse(ConfigurationManager.AppSettings["default.distance"]);
            }
            catch (Exception ex)
            {
                SimpleLogger.Logging.WriteToLog(ex.Message);
            }
        }

        public static double KCal { get; set; }
        public static double KTheta { get; set; }
        public static double Distance { get; set; }
    }
}
