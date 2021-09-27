using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;

using Goniometer_Controller.Functions;

namespace Goniometer_Controller.Models
{
    public class MeasurementCollection : BindingList<MeasurementBase>
    {
        protected override void InsertItem(int index, MeasurementBase item)
        {
            AssertUniqueValue(item);

            base.InsertItem(index, item);
        }

        public void AssertUniqueValue(MeasurementBase measurement) 
        {
            var existing = this.FirstOrDefault(m =>
                  m.Theta == measurement.Theta
                & m.Phi == measurement.Phi
                & m.Key == measurement.Key);

            if (existing != null)
                throw new Exception("Datapoint already in collection");
        }

        #region string manipulation
        public string ToCSV()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var value in this)
            {
                sb.AppendLine(MeasurementBase.ToCSV(value));
            }
            return sb.ToString();
        }

        public static MeasurementCollection FromCSV(string s)
        {
            var collection = new MeasurementCollection();

            string[] lines = s.Split('\n');
            foreach (string line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                var measurement = MeasurementBase.FromCSV(line);
                collection.Add(measurement);
            }
            return collection;
        }

        public static MeasurementCollection FromCSVFile(string fileName)
        {
            var stream = File.OpenText(fileName);
            
            string s = "";
            var collection = new MeasurementCollection();
            while ((s = stream.ReadLine()) != null)
            {
                collection.Add(MeasurementBase.FromCSV(s));
            }

            return collection;
        }
        #endregion
    }

    public static class MeasurementCollectionExtensions
    {
        #region estimates
        public static MeasurementBase GetEstimateReading(this MeasurementCollection source, string key, double theta, double phi)
        {
            //find closest vertical band
            double closeTheta = source
                .Where(m => m.Key == key)
                .MinSelectMember(m => Math.Abs(m.Theta - theta)).Theta;

            //find closest member
            return source
                .Where(m => m.Key == key & m.Theta == closeTheta)
                .MinSelectMember(t => Math.Abs(t.Phi - phi));
        }

        public static MeasurementBase GetEstimateReading_Extrapolation(this MeasurementCollection source, string key, double theta, double phi)
        {
            //try for an exact match:
            var match = source.FirstOrDefault(m => m.Key == key & m.Theta == theta & m.Phi == phi);
            if (match != null)
                return match;

            //if any vertical angles are valid, use them for linear extrapoliation
            var vMatches = source.Where(m => m.Key == key & m.Theta == theta);
            if (vMatches.Count() > 0)
            {
                //split points into those above and below vAngle, already proved there is not Value at exact vAngle
                var above = vMatches.Where(t => t.Theta > theta).ToList();
                var below = vMatches.Where(t => t.Theta < theta).ToList();

                if (above.Count == 0)
                {
                    //all values are below, return closest one
                    return below.OrderByDescending(t => t.Theta).First();
                }
                else if (below.Count == 0)
                {
                    //all values are above, return closest one
                    return above.OrderBy(t => t.Theta).First();
                }
                else
                {
                    var top = below.OrderByDescending(t => t.Theta).First();
                    var bot = above.OrderBy(t => t.Theta).First();

                    double estimate = LightMath.LinearExtrapolation(Tuple.Create(top.Theta, top.Value), Tuple.Create(bot.Theta, top.Value), theta);
                    return MeasurementBase.Create(theta, phi, theta, phi, key, estimate);
                }
            }

            //find 3 close values and 
            throw new NotImplementedException();
            //or not
        }
        #endregion

        #region operators
        public static MeasurementCollection SubstractStray(this MeasurementCollection source, MeasurementCollection stray)
        {
            MeasurementCollection results = new MeasurementCollection();

            foreach (var m in source)
            {
                double estimate = stray.GetEstimateReading(m.Key, m.Theta, m.Phi).Value;
                double value = (m.Value - estimate < 0) ? 0 : (m.Value - estimate);

                results.Add(MeasurementBase.Create(m.Theta, m.Phi, m.ExactTheta, m.ExactPhi, m.Key, value, m.SensorName, m.PortName));
            }

            return results;
        }

        private static MeasurementCollection SubtractFrom(this MeasurementCollection source, MeasurementCollection estimates)
        {
            MeasurementCollection results = new MeasurementCollection();

            foreach (var m in source)
            {
                double estimate = source.GetEstimateReading(m.Key, m.Theta, m.Phi).Value;
                results.Add(MeasurementBase.Create(m.Theta, m.Phi, m.ExactTheta, m.ExactPhi, m.Key, m.Value - estimate, m.SensorName, m.PortName));
            }

            return results;
        }

        public static MeasurementCollection MultiplyBy(this MeasurementCollection source, double value)
        {
            MeasurementCollection results = new MeasurementCollection();

            foreach (var m in source)
            {
                results.Add(MeasurementBase.Create(m.Theta, m.Phi, m.ExactTheta, m.ExactPhi, m.Key, m.Value * value, m.SensorName, m.PortName));
            }

            return results;
        }

        public static MeasurementCollection AveragePoles(this MeasurementCollection source)
        {
            MeasurementCollection results = new MeasurementCollection();

            foreach (string key in source.Select(m => m.Key).Distinct())
            {
                double north = 0;
                var norths = source.Where(m => m.Key == key & m.Phi == 180);
                if (norths.Count() > 0)
                    north = norths.Average(m => m.Value);

                double south = 0;
                var souths = source.Where(m => m.Key == key & m.Phi == 0);
                if (souths.Count() > 0)
                    south = souths.Average(m => m.Value);

                foreach (var m in source.Where(m => m.Key == key))
                {
                    if (m.Phi == 180)
                        results.Add(MeasurementBase.Create(m.Theta, m.Phi, m.ExactTheta, m.ExactPhi, m.Key, north, m.SensorName, m.PortName));
                    else if (m.Phi == 0)
                        results.Add(MeasurementBase.Create(m.Theta, m.Phi, m.ExactTheta, m.ExactPhi, m.Key, south, m.SensorName, m.PortName));
                    else
                        results.Add(m);
                }
            }

            return results;
        }
        #endregion

        #region conversions
        public static MeasurementCollection CalculateIntensity(this MeasurementCollection source, double distance)
        {
            string oldUnit = MeasurementKeys.Illuminance;
            string newUnit = MeasurementKeys.LuminousIntensity;

            MeasurementCollection results = new MeasurementCollection();

            foreach (var m in source.Where(m => m.Key == oldUnit))
            {
                double value = m.Value * Math.Pow(distance, 2);
                results.Add(MeasurementBase.Create(m.Theta, m.Phi, m.ExactTheta, m.ExactPhi, newUnit, value, m.SensorName, m.PortName));
            }

            return results;
        }
        #endregion
    }
}
