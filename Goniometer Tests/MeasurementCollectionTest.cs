using Goniometer_Controller.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace Goniometer_Tests
{
    /// <summary>
    ///This is a test class for MeasurementCollectionTest and is intended
    ///to contain all MeasurementCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MeasurementCollectionTest
    {
        /// <summary>
        ///A test for FromCSV
        ///</summary>
        [TestMethod()]
        public void FromCSVTest()
        {
            MeasurementCollection actual = GetRaw();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for FromCSV
        ///</summary>
        [TestMethod()]
        public void FromCSVStrayTest()
        {
            MeasurementCollection actual = GetRawStray();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetEstimateReading
        ///</summary>
        [TestMethod()]
        public void GetEstimateReadingTest()
        {
            MeasurementCollection target = GetRawStray();
            string key = MeasurementKeys.Illuminance;
            double theta = 0F; 
            double phi = 0F; 

            //based on values found in raw_stray
            double expectedTheta = 0;
            double expectedPhi = 0;
            double expectedValue = 0.008;

            MeasurementBase actual;
            actual = target.GetEstimateReading(key, theta, phi);

            Assert.AreEqual(key,            actual.Key);
            Assert.AreEqual(expectedTheta,  actual.Theta);
            Assert.AreEqual(expectedPhi,    actual.Phi);
            Assert.AreEqual(expectedValue,  actual.Value);
        }

        public static MeasurementCollection GetRaw()
        {
            string testfolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string testfile = "Example Data\\raw.csv";

            using (var sr = new StreamReader(testfolder + "\\" + testfile))
            {
                return MeasurementCollection.FromCSV(sr.ReadToEnd());
            }
        }

        public static MeasurementCollection GetRawStray()
        {
            string testfolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string testfile = "Example Data\\raw_stray.csv";

            using (var sr = new StreamReader(testfolder + "\\" + testfile))
            {
                return MeasurementCollection.FromCSV(sr.ReadToEnd());
            }
        }
    }
}
