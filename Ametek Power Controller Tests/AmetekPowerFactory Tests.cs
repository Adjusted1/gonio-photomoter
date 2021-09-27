using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ametek_Power_Controller;

namespace Ametek_Power_Controller_Tests
{
    [TestClass]
    public class AmetekPowerFactoryTests
    {
        [TestMethod]
        public void GetAmetekControllerTest()
        {
            var controller = AmetekPowerFactory.GetController();
            float[] measurements = controller.Measurement.Array.Measure(Ametek.AmetekACPwr.Interop.AmetekACPwrArrayMeasurementEnum.AmetekACPwrArrayMeasurementVoltage, -2);

            Assert.IsNotNull(measurements);
        }
    }
}
