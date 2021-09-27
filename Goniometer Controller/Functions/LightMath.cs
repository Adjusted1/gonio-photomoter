using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Goniometer_Controller.Models;

namespace Goniometer_Controller.Functions
{
    public static class LightMath
    {
        #region general algorithms
        public static double Radians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static double SolidAngle(double degrees)
        {
            return 2 * Math.PI * (1 - Math.Cos(Radians(degrees)));
        }

        /// <summary>
        /// Gives the width between the midpoint of each sequential pair of values.
        /// </summary>
        /// <param sensorname="values">sorted list</param>
        /// <returns></returns>
        public static double[] WidthOnCenter(double[] values)
        {
            double[] widths = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                //get midrange between this and previous angle
                double theta1;
                if (i == 0)
                    theta1 = values[i];
                else
                    theta1 = values[i] - (values[i] - values[i - 1]) / 2;

                //get midrange between this and next angle
                double theta2;
                if (i == values.Length - 1)
                    theta2 = values[i];
                else
                    theta2 = values[i] + (values[i + 1] - values[i]) / 2;

                widths[i] = theta2 - theta1;
            }
            return widths;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param sensorname="p1">x1, y1</param>
        /// <param sensorname="p2">x2, y2</param>
        /// <param sensorname="x">x0</param>
        /// <returns>y0</returns>
        public static double LinearExtrapolation(Tuple<double, double> p1, Tuple<double, double> p2, double x)
        {
            double m = (p1.Item2 - p2.Item2) / (p1.Item1 - p2.Item1);
            double c = m * p1.Item1 - p1.Item2;

            return m * x + c;
        }

        public static double BiLinearExtrapolation(
            Tuple<double, double, double> p1, Tuple<double, double, double> p2,
            Tuple<double, double, double> p3, Tuple<double, double, double> p4,
            double x, double y)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region summation methods
        /// <summary>
        /// Calculate the Lumen Value of a vertical array and weight them by horizontal angle
        /// </summary>
        /// <param sensorname="data">Units: hAngle degrees, vAngle degrees, footcandles</param>
        /// <returns></returns>
        public static double CalculateLumensByVertical(List<Tuple<double, double, double>> data)
        {
            double totalLumens = 0;

            double[] hAngles = data.Select((item) => item.Item1).Distinct().OrderBy(a => a).ToArray();
            double[] hWidths = WidthOnCenter(hAngles);
            double hRange = hAngles[hAngles.Length - 1] - hAngles[0];

            for (int h = 0; h < hAngles.Length; h++)
            {
                var vData = data.Where((item) => item.Item1 == hAngles[h])
                                .Select((item) => Tuple.Create(item.Item2, item.Item3))
                                .ToList();

                double lumen = CalculateLumens(vData);
                lumen *= hWidths[h] / hRange;   //weight the measurement

                totalLumens += lumen;           //sum weighted measurement
            }

            return totalLumens;
        }

        /// <summary>
        /// Average weighted horizontal readings, then calculate along surface angle
        /// </summary>
        /// <param name="measurements"></param>
        /// <returns></returns>
        public static double CalculateLumens(IEnumerable<MeasurementBase> measurements)
        {
            string candleKey = MeasurementKeys.LuminousIntensity;
            var candles = measurements.Where(m => m.Key == candleKey)
                            .Select(m => Tuple.Create(m.Theta, m.Phi, m.Value))
                            .ToList();

            return CalculateLumens(candles);
        }

        /// <summary>
        /// Average weighted horizontal readings, then calculate along surface angle
        /// </summary>
        /// <param sensorname="data">Units: hAngle degrees, vAngle degrees, footcandles</param>
        /// <returns></returns>
        public static double CalculateLumens(List<Tuple<double, double, double>> data)
        {
            //vAngle, footcandle
            var averagedReadings = new List<Tuple<double, double>>();

            double[] vRange = data.Select((item) => item.Item2).Distinct().ToArray();
            for (int v = 0; v < vRange.Length; v++)
            {
                //select just those values in the vertical range
                var cross = data.Where((item) => item.Item2 == vRange[v]).ToList();

                double averageCandles = 0;

                double[] hAngles = cross.Select((item) => item.Item1).Distinct().OrderBy(a => a).ToArray();
                double[] hWidths = hAngles.Length == 1 ? new double[] { 360 } : WidthOnCenter(hAngles);
                double hRange = hAngles.Length == 1 ? 360 : hAngles[hAngles.Length - 1] - hAngles[0];

                for (int h = 0; h < hAngles.Length; h++)
                {
                    double weightedCandle = cross[h].Item3;

                    weightedCandle *= hWidths[h] / hRange;   //weight the measurement
                    averageCandles += weightedCandle;        //sum weighted measurement
                }

                averagedReadings.Add(Tuple.Create(vRange[v], averageCandles));
            }

            return CalculateLumens(averagedReadings);
        }

        /// <summary>
        /// Calculate Lumen data from angular data
        /// </summary>
        /// <param sensorname="data">Units: degrees, footcandles</param>
        /// <returns></returns>
        public static double CalculateLumens(List<Tuple<double, double>> data)
        {
            //must be sorted by angle
            data.Sort();

            double total = 0;

            for (int i = 0; i < data.Count; i++)
            {
                //get midrange between this and previous angle
                double theta1;
                if (i == 0)
                    theta1 = data[i].Item1;
                else
                    theta1 = data[i].Item1 - (data[i].Item1 - data[i - 1].Item1) / 2;

                //get midrange between this and next angle
                double theta2;
                if (i == data.Count - 1)
                    theta2 = data[i].Item1;
                else
                    theta2 = data[i].Item1 + (data[i + 1].Item1 - data[i].Item1) / 2;

                //determine solid angle for this band
                double s = SolidAngle(theta2) - SolidAngle(theta1);

                //sum component values
                total += data[i].Item2 * s;
            }

            return total;
        } 
        #endregion

        #region luminous preparations
        //this really needs a better name
        public static MeasurementCollection PrepareLuminousMeasurements(MeasurementCollection source, double distance, double kCal, double kTheta)
        {
            //convert any candle values to candelas
            source = source.CalculateIntensity(distance);

            //adjust values by calibration factor
            source = source.MultiplyBy(kCal);

            //adjust values by theta factor
            source = source.MultiplyBy(kTheta);

            //average nadir
            source = source.AveragePoles();

            return source;
        }
        #endregion
    }
}
