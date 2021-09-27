using System;
using System.Collections.Generic;
using System.IO.Ports;

using Minolta_Controller.Sensors;

namespace Minolta_Controller
{
    public static class MinoltaSensorFactory
    {
        static MinoltaSensorFactory()
        {
        }

        public static IEnumerable<string> GetSensorTypes()
        {
            var types = new List<string>();

            types.Add(MinoltaT10Controller.Type);
            types.Add(MinoltaCL200Controller.Type);

            return types;
        }

        public static MinoltaBaseSensor CreateSensor(string name, string type, SerialPort port)
        {
            MinoltaBaseSensor sensor;

            switch (type)
            {
                case (MinoltaT10Controller.Type):
                    sensor = new MinoltaT10Controller(port);
                    break;

                case (MinoltaCL200Controller.Type):
                    sensor = new MinoltaCL200Controller(port);
                    break;

                default:
                    throw new ArgumentException("Unknown Sensor by that sensorname");
            }

            sensor.Name = name;
            sensor.Connect();

            return sensor;
        }    
    }
}
