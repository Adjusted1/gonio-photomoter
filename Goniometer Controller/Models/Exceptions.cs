using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Models
{
    public class InvalidMeasurementException : Exception
    {
        /// <summary>
        /// invalid measurement collected during this event
        /// </summary>
        public MeasurementBase Measurement;

        public InvalidMeasurementException(MeasurementBase measurement)
        {
            this.Measurement = measurement;
        }
    }
}
