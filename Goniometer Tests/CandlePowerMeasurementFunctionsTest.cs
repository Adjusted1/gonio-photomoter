using Goniometer_Controller.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Goniometer_Tests
{
    
    
    /// <summary>
    ///This is a test class for CandlePowerMeasurementFunctionsTest and is intended
    ///to contain all CandlePowerMeasurementFunctionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CandlePowerMeasurementFunctionsTest
    {
        /// <summary>
        ///A test for CalculateIntensity
        ///</summary>
        [TestMethod()]
        public void CalculateIntensityTest()
        {
            MeasurementCollection readings = MeasurementCollectionTest.GetRaw();
            double distance = 19.3333;

            MeasurementCollection actual = readings.CalculateIntensity(distance);
            Assert.IsNotNull(actual);
        }
    }
}
