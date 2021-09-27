using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Goniometer_Controller.Models;

namespace Goniometer_Controller.Sensors
{
    public abstract class BaseSensor : IDisposable
    {
        public string Name { get; set; }

        public abstract IEnumerable<MeasurementBase> CollectMeasurements(double theta, double phi, double exactTheta, double exactPhi);

        public abstract void Dispose();
    }
}
