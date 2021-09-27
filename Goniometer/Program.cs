using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Goniometer_Controller.Motors;

namespace Goniometer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //rotate the old logs each run
            SimpleLogger.Logging.RotateLogFile();

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                SimpleLogger.Logging.WriteToLog(ex.Message);
                MotorController.EmergencyStop();
                throw;
            }
        }
    }
}
