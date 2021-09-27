using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Models
{
    public class ChrominanceMeasurement 
    {
        /// <summary>
        /// Illuminance, candels
        /// </summary>
        public double CandlePower;
        
        /// <summary>
        /// blue luma
        /// </summary>
        public double U;

        /// <summary>
        /// red luma
        /// </summary>
        public double V;
    }
}
