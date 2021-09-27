using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Goniometer;
using Goniometer_Controller;
using Goniometer_Controller.Sensors;
using Minolta_Controller;

namespace Goniometer.Sensors
{
    public static class SensorProvider
    {
        private static object _sensorsLock = new object();
        private static List<BaseSensor> _sensors = new List<BaseSensor>();

        public static void LoadSensorConfiguration()
        {
            //reload sensor list
            _sensors = new List<BaseSensor>();
            var config = GoniometerConfigurationSection.GetConfigurationSection();

            foreach (GoniometerConfigurationSection.SensorConfigurationElement sensorInfo in config.Sensors)
            {
                var port = SerialPortProvider.GetPort(sensorInfo.Port);
                var sensor = MinoltaSensorFactory.CreateSensor(sensorInfo.Name, sensorInfo.Type, port);
                
                AddSensor(sensor);
            }
        }

        public static void SaveSensorConfiguration()
        {

        }

        public static void AddSensor(BaseSensor sensor)
        {
            lock (_sensorsLock)
            {
                _sensors.Add(sensor);
            }
        }

        public static bool RemoveSensor(BaseSensor sensor)
        {
            lock (_sensorsLock)
            {
                return _sensors.Remove(sensor);
            }
        }

        public static IEnumerable<BaseSensor> GetSensors()
        {
            return _sensors.ToList();
        }
    }
}
