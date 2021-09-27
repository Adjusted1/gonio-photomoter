using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Reflection;

using Goniometer_Controller.Functions;
using Goniometer_Controller.Models;

namespace Goniometer_Tests
{
    [TestClass()]
    public class LightMathTest
    {
        #region lumen calibration values
        public static double Distance
        {
            get { return Double.Parse(ConfigurationManager.AppSettings["default.distance"]); }
        }

        public static double KCal
        {
            get { return Double.Parse(ConfigurationManager.AppSettings["default.correction.calibration"]); }
        }

        public static double KTheta
        {
            get { return Double.Parse(ConfigurationManager.AppSettings["default.correction.theta"]); }
        }
        #endregion

        /// <summary>
        ///A test for CalculateLumensByHorizontalAverage
        ///</summary>
        [TestMethod()]
        public void CalculateLumensByHorizontalAverageTest()
        {
            string testfolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] testfiles = { "Example Data\\exampledata.txt", "Example Data\\exampledata2.txt" };

            foreach (string testfile in testfiles)
            {
                double expected;
                double actual;
                var data = GenerateData(testfolder + "//" + testfile, out expected);

                actual = LightMath.CalculateLumens(data);
                Assert.AreEqual(expected, actual, 0.1);
            }
        }

        /// <summary>
        ///A test for CalculateLumensByVertical
        ///</summary>
        [TestMethod()]
        public void CalculateLumensByVerticalTest()
        {
            string testfolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] testfiles = { "Example Data\\exampledata.txt", "Example Data\\exampledata2.txt" };

            foreach (string testfile in testfiles)
            {
                double expected;
                double actual;
                var data = GenerateData(testfolder + "//" + testfile, out expected);

                actual = LightMath.CalculateLumensByVertical(data);
                Assert.AreEqual(expected, actual, 0.1);
            }
        }

        [TestMethod()]
        public void CalculateLumensTest()
        {
            MeasurementCollection source = GetLuminousData();

            string key = MeasurementKeys.LuminousIntensity;
            var target = source
                .Where(m => m.Key == key)
                .Select(m => Tuple.Create(m.Theta, m.Phi, m.Value))
                .ToList();

            double lumens = LightMath.CalculateLumens(target);
            
            double actual = 439;
            double delta = actual * 0.04; // 4.0% accuracy requirement
            Assert.AreEqual(actual, lumens, delta);
        }

        #region example data
        public static MeasurementCollection GetLuminousData()
        {
            var lightData = GetLuminousRawData();
            var strayData = GetLuminousStrayRawData();

            //calculate corrected values from stray
            return lightData.SubstractStray(strayData);
        }

        public static MeasurementCollection GetLuminousRawData()
        {
            var lightData = MeasurementCollectionTest.GetRaw();
            return LightMath.PrepareLuminousMeasurements(lightData, Distance, KCal, KTheta);
        }

        public static MeasurementCollection GetLuminousStrayRawData()
        {
            var strayData = MeasurementCollectionTest.GetRawStray();
            return LightMath.PrepareLuminousMeasurements(strayData, Distance, KCal, KTheta);
        } 
        #endregion

        private List<Tuple<double, double, double>> GenerateData(string filename, out double lumens)
        {
            var results = new List<Tuple<double, double, double>>();

            string hValues, vValues, readings;
            using (var sr = new StreamReader(filename))
            {
                lumens = Double.Parse(sr.ReadLine());

                vValues = sr.ReadLine();
                hValues = sr.ReadLine();

                readings = sr.ReadToEnd();
            }

            double[] hRange = hValues.Split(',').Select(h => Double.Parse(h)).ToArray();
            double[] vRange = vValues.Split(',').Select(v => Double.Parse(v)).ToArray();

            string[] lines = readings.Split('\n');
            for (int h = 0; h < lines.Length; h++ )
            {
                string[] values = lines[h].Split(',');
                for (int v = 0; v < values.Length; v++)
                {
                    double result = Double.Parse(values[v]);
                    results.Add(Tuple.Create(hRange[h], vRange[v], result));
                }
            }

            return results;
        }
    }
}
