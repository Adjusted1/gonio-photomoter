using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Threading;

using SimpleLogger;

using Goniometer_Controller.Models;

namespace Minolta_Controller.Sensors
{
    public class MinoltaCL200Controller : MinoltaBaseSensor
    {
        public const string Type = "Minolta CL200";

        #region construction
        public MinoltaCL200Controller(SerialPort port)
            : base(port)
        {
        }

        /// <summary>
        /// Open SerialPort if Closed
        /// Set sensor to PC Mode, Hold Mode, and EXT Mode
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            //command 54 = set device to pc mode
            //data = 1\s\s\s = Connect
            //[Connect][Empty][Empty][Empty]
            SendCommand(0, 54, "1   ");
            Thread.Sleep(500);

            //read out confirmation
            _port.ReadLine();
            
            //command 55 = set hold status
            //data = 1\s\s0 = 
            //[][][][]
            SendCommand(99, 55, "1  0");
            Thread.Sleep(500);
            
            /* no response to read */

            //command 40 = set EXT mode
            //data = 10\s\s = EXT MODE
            //[][][][]
            SendCommand(0, 40, "10  ");
            Thread.Sleep(175);

            //read out confirmation
            //TODO: check error code
            _port.ReadLine();
        }

        public override bool TestStatus()
        {
            return true;
        }
        #endregion

        #region measurement methods
        /// <summary>
        /// Take a measurement on all receptors. Call this before using the read commands
        /// </summary>
        public void TakeMeasurement()
        {
            int receptor = 99;      //send command to all heads
            int command = 40;       //EXT Command
            string data = "21  ";   //"21\s\s"

            SendCommand(receptor, command, data);
            Thread.Sleep(500);
        }

        public void ReadXYZ(int receptor, bool useCF, CalibrationModeEnum mode,
            out double x, out double y, out double z)
        {
            int command = 1;
            ReadMeasurement(receptor, command, useCF, mode, out x, out y, out z);
        }

        public void ReadEvXY(int receptor, bool useCF, CalibrationModeEnum mode,
            out double Ev, out double x, out double y)
        {
            try
            {
                int command = 2;
                ReadMeasurement(receptor, command, useCF, mode, out Ev, out x, out y);
            }
            catch (LowIlluminanceException)
            {
                Ev = x = y = 0;
            }
        }

        public void ReadEvUV(int receptor, bool useCF, CalibrationModeEnum mode,
            out double Ev, out double u, out double v)
        {
            try
            {
                int command = 3;
                ReadMeasurement(receptor, command, useCF, mode, out Ev, out u, out v);
            }
            catch (LowIlluminanceException)
            {
                Ev = u = v = 0;
            }
        }

        public void ReadEvTcpUV(int receptor, bool useCF, CalibrationModeEnum mode,
            out double Ev, out double Tcp, out double uv)
        {
            try
            {
                int command = 8;
                ReadMeasurement(receptor, command, useCF, mode, out Ev, out Tcp, out uv);
            }
            catch (LowIlluminanceException)
            {
                Ev = Tcp = uv = 0;
            }
        }

        public void ReadEvDWP(int receptor, bool useCF, CalibrationModeEnum mode,
            out double Ev, out double dw, out double p)
        {
            int command = 15;
            ReadMeasurement(receptor, command, useCF, mode, out Ev, out dw, out p);
        }

        public void ReadX2YZ(int receptor, bool useCF, CalibrationModeEnum mode,
            out double x2, out double y, out double z)
        {
            int command = 45;
            ReadMeasurement(receptor, command, useCF, mode, out x2, out y, out z);
        }

        private void ReadMeasurement(int receptor, int command, bool useCF, CalibrationModeEnum mode,
            out double r1, out double r2, out double r3)
        {
            string data = "1";
            data += useCF ? "2" : "3";
            data += "0";

            if (mode == CalibrationModeEnum.NORM)
                data += "0";
            else
                data += "1";

            SendCommand(receptor, command, data);
            string res = ReadResponse(out receptor, out command);

            string unknw = res.Substring(0, 1);     //"1" or "5"
            string error = res.Substring(1, 1);
            string range = res.Substring(2, 1);
            string batLv = res.Substring(3, 1);     //battery level, 0 == normal

            //check error code
            ErrorCheckChar(error);

            //measurement out of range
            if (range == "0")
                throw new Exception("Range not determined");

            if (range == "6")
                throw new Exception("Out of Range Error");

            //reading 1
            if (res.Substring(4, 6) == " ---- ")
                throw new LowIlluminanceException();
            else
                r1 = ParseReadingValue(res.Substring(4, 6));

            //reading 2
            if (res.Substring(10, 6) == " ---- ")
                throw new LowIlluminanceException();
            else
                r2 = ParseReadingValue(res.Substring(10, 6));

            //reading 3
            if (res.Substring(16, 6) == " ---- ")
                throw new LowIlluminanceException();
            else
                r3 = ParseReadingValue(res.Substring(16, 6));
        }

        protected override void ErrorCheckChar(string error)
        {
            //error information
            switch (error)
            {
                case " ":
                    //normal operation
                    break;

                case "1":
                    throw new Exception("Receptor Head Off");

                case "2":
                    throw new Exception("EEPROM Error 1");

                case "3":
                    throw new Exception("EEPROM Error 2");

                case "4":
                    throw new Exception("EXT Error");

                case "5":
                    throw new Exception("Measurement over Value Error");

                case "6":
                    break;
                    //throw new LowIlluminanceException();

                case "7":
                    break;
                    //throw new Exception("Value out of range Error");

                default:
                    throw new Exception("Unknown Error");
            }
        }
        #endregion

        public override IEnumerable<MeasurementBase> CollectMeasurements(double theta, double phi, double exactTheta, double exactPhi)
        {
            //timeout values
            TimeSpan timeout = new TimeSpan(0, 1, 0);
            DateTime startTime = DateTime.Now;

            //failure rates
            int fails = 0;
            int maxFails = 2;

            //reading values
            double eps = 0.02;
            bool useCF = false;
            CalibrationModeEnum mode = CalibrationModeEnum.NORM;

            //Take two measurements. Make sure they are similar within some epsilon. Return the second measurement
            //If the measurements differ significantly, start over
            List<MeasurementBase> m1, m2 = null;
            bool valid;

            do
            {
                valid = true;

                //check if we've taken too long
                if (DateTime.Now - startTime > timeout)
                    throw new TimeoutException("CollectMeasurements timed out.");

                //ignore first few exceptions
                try
                {
                    //rotate m2 => m1, take new measurements
                    m1 = m2;
                    m2 = CollectMeasurements(theta, phi, exactTheta, exactPhi, 0, useCF, mode);

                    //if this is the first iteration, m1 will be null
                    if (m1 == null || m2 == null)
                    {
                        valid = false;
                        continue;
                    }

                    //compare for each Key
                    foreach (string key in m1.Select(m => m.Key))
                    {
                        double v1 = m1.First(m => m.Key == key).Value;
                        double v2 = m2.First(m => m.Key == key).Value;

                        //validate zero measurements must be identical
                        //as we don't know what the scale is
                        if (v1 == 0 && v2 != 0)
                            valid = false;

                        //validate measurement within some epsilon
                        if (v2 != 0 && Math.Abs((v1 - v2) / v2) > eps)
                            valid = false;
                    }
                }
                catch (TimeoutException)
                {
                    if (fails < maxFails)
                    {
                        valid = false;
                        fails++;
                        this.Connect();
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception)
                {
                    if (fails < maxFails)
                    {
                        valid = false;
                        fails++;
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }
            
            } while (!valid);

            //measurements validated, return most recent measurement
            return m2;
        }

        private List<MeasurementBase> CollectMeasurements(double theta, double phi, double exactTheta, double exactPhi, int receptor, bool useCF, CalibrationModeEnum mode)
        {
            string name = this.Name;
            string port = this._port.PortName;

            var measurements = new List<MeasurementBase>();
            TakeMeasurement();

            double Ev1, u, v;
            ReadEvUV(receptor, useCF, mode, out Ev1, out u, out v);
            //measurements.Add(MeasurementBase.Create(theta, phi, MeasurementKeys.Illuminance, Ev1, name, port));
            measurements.Add(MeasurementBase.Create(theta, phi, exactTheta, exactPhi, MeasurementKeys.ColorU, u, name, port));
            measurements.Add(MeasurementBase.Create(theta, phi, exactTheta, exactPhi, MeasurementKeys.ColorV, v, name, port));

            double Ev2, Tcp, Duv;
            ReadEvTcpUV(receptor, useCF, mode, out Ev2, out Tcp, out Duv);
            measurements.Add(MeasurementBase.Create(theta, phi, exactTheta, exactPhi, MeasurementKeys.ColorTemp, Tcp, name, port));
            measurements.Add(MeasurementBase.Create(theta, phi, exactTheta, exactPhi, MeasurementKeys.ColorDiff, Duv, name, port));

            return measurements;
        }

        public enum CalibrationModeEnum
        {
            NORM,
            MULTI,
        }

        /// <summary>
        /// Throw when the reading is below the Detectable Limit
        /// </summary>
        public class LowIlluminanceException : Exception
        {
        }
    }
}
