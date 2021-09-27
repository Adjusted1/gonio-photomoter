using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

using SimpleLogger;

using Goniometer_Controller.Models;

namespace Minolta_Controller.Sensors
{
    public class MinoltaT10Controller : MinoltaBaseSensor, INotifyPropertyChanged
    {
        public const string Type = "Minolta T10";

        private int _refreshRate = 100; //milliseconds
        private Timer _updateTimer;
        private Int64 _measureCount = 0;

        #region construction
        public MinoltaT10Controller(SerialPort port)
            : base(port)
        { }

        public override void Connect()
        {
            base.Connect();

            //command 54 = set device to pc mode
            //data = 1\s\s\s = Connect
            //[Connect][Empty][Empty][Empty]
            SendCommand(0, 54, "1   ");
            Thread.Sleep(1000);

            //read out confirmation
            _port.ReadLine();

            //99 = send to all receptors
            //command 55 = set hold to all connected recptors
            //data = 1  1 = Run, Slow
            //[Run/Hold][Empty][Empty][Fast/Slow]
            SendCommand(99, 55, "1  1");
            Thread.Sleep(5000);
            
            /* no response to read */

            //start taking measurements
            status = "Starting Measurement";
            _updateTimer = new Timer(UpdateReading, this, 0, _refreshRate);
        }

        public override void Disconnect()
        {
            base.Disconnect();

            if (_updateTimer != null)
                _updateTimer.Dispose();
        }

        public override bool TestStatus()
        {
            return true;
        }
        #endregion

        private double Read()
        {
            //00 = send to first receptor
            //command 10 = read illuminance
            //data = "0220" = Run, CCF_Off, Range..., Fast
            //[Run/Hold][CCF][Range][Fast/Slow]

            SendCommand(0, 10, "0200");
            return ReadResult();
        }

        private double ReadResult()
        {
            string res = _port.ReadLine();

            /* 
             * res.Substring(0, 1);    //Start of Text     \u0002
             * res.Substring(1, 2);    //Receptor #        "00"
             * res.Substring(3, 2);    //Command #         "10"
             * 
             * //Data 4, Reading Metadata
             * res.Substring(5, 1);    //HOLD status       "0, 2, 4, 6" or "1, 3, 5, 7"
             * res.Substring(6, 1);    //Error Info
             * res.Substring(7, 1);    //Range             "0" or "1 - 5"
             * res.Substring(8, 1);    //Battery Info      "0, 2"
             * 
             * //Data 3, Illuminance Value
             * res.Substring(9, 6);
             * 
             * //Data 2, Delta Value
             * res.Substring(15, 6);   
             * 
             * //Data 1, % Value
             * res.Substring(21, 6);   
             * 
             * res.Substring(27, 1);   //End of Text       \u0003
             * 
             * res.Substring(28, 2);   //BCC
             * 
             * res.Substring(30, 2);   //newline
             * */

            //length validation
            //31 or 32 depending on if the system considers the newline one or two chars
            if (res.Length != 31 & res.Length != 32)
                throw new Exception("Message Malformed");

            //REMOVE, the device doesn't provide valid checksums back
            //checksum validation
            string bcc = BlockCheckChar(res.Substring(1, 27));
            if (bcc != res.Substring(28, 2))
                throw new Exception("Message Malformed");

            //error informaiton
            if (res.Substring(6, 1) != " ")
                throw new Exception("Device Error Reported");
            
            return ParseReadingValue(res.Substring(9, 6));
        }

        private double _reading;
        public double reading
        {
            get
            {
                return this._reading;
            }
            set
            {
                if (_reading != value)
                {
                    this._reading = value;
                    NotifyPropertyChanged("reading");
                }
            }
        }

        private string _status;
        public string status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (_status != value)
                {
                    this._status = value;
                    NotifyPropertyChanged("status");
                }
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            var notify = PropertyChanged;
            if (notify != null)
            {
                notify(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        private object _lock = new object();
        private void UpdateReading(object state)
        {
            if (Monitor.TryEnter(_lock))
            {
                try
                {
                    reading = Read();
                    _measureCount++;

                    //check for rollover
                    if (_measureCount < 0)
                        _measureCount = 0;
                }
                catch (TimeoutException tEx)
                {
                    try
                    {
                        this.Connect();
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteToLog(tEx.Message);
                        Logging.WriteToLog(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    Logging.WriteToLog(ex.Message);
                }                
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }

        public override IEnumerable<MeasurementBase> CollectMeasurements(double theta, double phi, double exactTheta, double exactPhi)
        {
            double eps = 0.02;
            MeasurementBase m1, m2;
            bool valid;

            //wait for signal to stabilize
            Thread.Sleep(500);

            //Take two measurements. Make sure they are similar within some epsilon. Return the first measurement
            //If the measurements differ significantly, start over
            do
            {
                //start over!
                valid = true;
                m1 = CollectMeasurement(theta, phi, exactTheta, exactPhi);
                m2 = CollectMeasurement(theta, phi, exactTheta, exactPhi);

                if (Math.Abs(m1.Value - m2.Value) > eps)
                    valid = false;

            } while (!valid);

            return new List<MeasurementBase> { m1 };
        }

        private MeasurementBase CollectMeasurement(double theta, double phi, double exactTheta, double exactPhi)
        {
            //timeout
            TimeSpan timeout = new TimeSpan(0, 1, 0);
            DateTime start = DateTime.Now;

            //save current count
            var before = _measureCount;
            while (before == _measureCount)
            {
                Thread.Sleep(_refreshRate);

                if (DateTime.Now > start + timeout)
                    throw new TimeoutException(String.Format("Waiting on new measurement for {0} on {1} took too long.", this.Name, this._port.PortName));
            }

            return MeasurementBase.Create(theta, phi, exactTheta, exactPhi, MeasurementKeys.Illuminance, reading, this.Name, this._port.PortName);
        }
    }
}
