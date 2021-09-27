using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Models
{
    [Serializable]
    public class MeasurementBase
    {
        private readonly double _theta;
        /// <summary>
        /// Horizontal/Azimuthal angle from lamp reference
        /// </summary>
        public double Theta { get { return _theta; } }

        private readonly double _exactTheta;
        /// <summary>
        /// Horizontal/Azimuthal angle from lamp reference, measured by encoder value
        /// </summary>
        public double ExactTheta { get { return _exactTheta; } }

        private readonly double _phi;
        /// <summary>
        /// Vertical Angle from bottom
        /// </summary>
        public double Phi { get { return _phi; } }

        private readonly double _exactPhi;
        /// <summary>
        /// Vertical Angle from bottom, measured by encoder value
        /// </summary>
        public double ExactPhi { get { return _exactPhi; } }

        private readonly string _key;
        public string Key { get { return _key; } }

        private readonly double _value;
        public double Value { get { return _value; } }

        private readonly string _sensorName;
        public string SensorName { get { return _sensorName; } }

        private readonly string _portName;
        public string PortName { get { return _portName; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theta">Horizontal/Azimuthal angle from lamp reference</param>
        /// <param name="phi">Vertical Angle from bottom</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="sensorName"></param>
        /// <param name="portName"></param>
        public MeasurementBase(double theta, double phi, double exactTheta, double exactPhi,
            string key, double value, string sensorName = "", string portName = "")
        {
            this._theta = theta;
            this._phi = phi;
            this._exactTheta = exactTheta;
            this._exactPhi = exactPhi;
            this._key = key;
            this._value = value;
            this._sensorName = sensorName;
            this._portName = portName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theta">Horizontal/Azimuthal angle from lamp reference</param>
        /// <param name="phi">Vertical Angle from bottom</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="sensorname"></param>
        /// <param name="portname"></param>
        /// <returns></returns>
        public static MeasurementBase Create(double theta, double phi, double exactTheta, double exactPhi,
            string key, double value, string sensorname = "", string portname = "")
        {
            return new MeasurementBase(theta, phi, exactTheta, exactPhi, key, value , sensorname, portname);
        }

        public static string ToCSV(MeasurementBase measurement)
        {
            string s = "";
            s += measurement.SensorName + ",";
            s += measurement.PortName   + ",";
            s += measurement.Theta      + ",";
            s += measurement.Phi        + ",";
            s += measurement.ExactTheta + ",";
            s += measurement.ExactPhi   + ",";
            s += measurement.Key        + ",";
            s += measurement.Value;
            return s;
        }

        public static MeasurementBase FromCSV(string measurement)
        {
            string[] values = measurement.Split(',');
            if (values.Length != 8)
                throw new ArgumentException("Expected comma separated string with 6 values");

            string sensorName = values[0];
            string portName   = values[1];
            double theta      = Double.Parse(values[2]);
            double phi        = Double.Parse(values[3]);
            double exactTheta = Double.Parse(values[4]);
            double exactPhi   = Double.Parse(values[5]);
            string key        = values[6];
            double value      = Double.Parse(values[7]);

            return MeasurementBase.Create(theta, phi, exactTheta, exactPhi, key, value, sensorName, portName);
        }
    }
}
