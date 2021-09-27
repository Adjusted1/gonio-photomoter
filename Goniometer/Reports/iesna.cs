using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Goniometer.Functions;
using Goniometer_Controller;
using Goniometer_Controller.Models;
using Goniometer_Controller.Functions;

namespace Goniometer.Reports
{
    /// <summary>
    /// ANSI LM-63-2002
    /// </summary>
    public class iesna
    {
        #region required keywords
        public string TestName;
        public string TestLab;
        public DateTime IssueDate;
        public string Manufacturer;
        public string Model;
        public string Wattage;
        public string Length;
        public string Width;
        public string Height;
        #endregion

        #region optional keywords
        public string lumcat;       //luminaire catalog number
        public string luminaire;    //luminaire description
        public string lampcat;      //lamp catalog number
        public string lamp;         //lamp description (i.e., type, wattage, size, etc.)

        public string ballastcat;
        public string ballast;

        public string maintcat;
        public string other;
        public string more;
        
        public string lampPosition;
        public string search;

        public Dictionary<string, string> comments;
        #endregion

        private MeasurementCollection _data;

        public iesna(MeasurementCollection data)
        {
            _data = data;

            //average nadir
            _data = _data.AveragePoles();
        }

        /// <summary>
        /// write report to file according to ansi standards
        /// </summary>
        /// <param sensorname="report"></param>
        /// <param sensorname="filefolder"></param>
        /// <returns>fullpath to created file</returns>
        public static string WriteToFile(iesna report, string fileFolder)
        {
            string fileName = fileFolder + String.Format("/iesna_{0:yyyyMMdd} {1} {2}.ies", DateTime.Now, report.TestName, report.Model);

            using (var fs = new FileStream(fileName, FileMode.CreateNew))   //never overwrite a previous file
            using (var sw = new StreamWriter(fs, Encoding.ASCII))           //standard requires ansii
            {
                //TODO implement max column lengths of 256 char including /n

                sw.Write(report.ToString());
                sw.Flush();
            }

            return fileName;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("IESNA:LM-63-2002");
            sb.AppendLine("[TEST]"          + TestName                            );
            sb.AppendLine("[TESTLAB]"       + TestLab                             );
            sb.AppendLine("[ISSUEDATE]"     + IssueDate.ToString("dd-MMM-yyyy")   );
            sb.AppendLine("[MANUFAC]"       + Manufacturer                        );
            sb.AppendLine("[LUMCAT]"        + lumcat                              );
            sb.AppendLine("[LUMINAIRE]"     + luminaire                           );
            sb.AppendLine("[LAMPCAT]"       + lampcat                             );
            sb.AppendLine("[LAMP]"          + lamp                                );
            sb.AppendLine("[BALLASTCAT]"    + ballastcat                          );
            sb.AppendLine("[BALLAST]"       + ballast                             );
            sb.AppendLine("[MAINTCAT]"      + maintcat                            );
            sb.AppendLine("[OTHER]"         + other                               );
            sb.AppendLine("[MORE]"          + more                                );
            sb.AppendLine("[LAMPPOSITION]"  + lampPosition                        );
            sb.AppendLine("[SEARCH]"        + search                              );

            if (comments != null && comments.Count > 0)
                foreach (var key in comments.Keys)
                    sb.AppendLine(String.Format("[_{0}] {1}", key.ToUpper(), comments[key]));

            //tilt factor
            sb.AppendLine("TILT=NONE");
            sb.AppendLine("1");
            
            //fetch all candle readings
            string candleKey = MeasurementKeys.LuminousIntensity;
            var candles = _data.Where(m => m.Key == candleKey)
                               .Select(m => Tuple.Create(m.Theta, m.Phi, m.Value))
                               .ToList();

            //lumen reading
            double lumen = LightMath.CalculateLumens(candles);
            sb.AppendLine(String.Format("{0:0.##}", lumen));

            //scaling factor
            sb.AppendLine("1.000001");

            //vertical step count
            double[] vRange = _data
                .Select(m => m.Phi)
                .Distinct()
                .OrderBy(v => v)
                .ToArray();
            sb.AppendLine(vRange.Length.ToString());

            //horizontal step count
            double[] hRange = _data
                .Select(m => m.Theta)
                .Distinct()
                .OrderBy(h => h)
                .ToArray();
            sb.AppendLine(hRange.Length.ToString());

            //unknown, always 1
            sb.AppendLine("1");

            //units 1:feet, 2:meters
            sb.AppendLine("1");

            //fixture size, length, width, height
            sb.AppendLine(String.Format("{0} {1} {2}", Length, Width, Height));

            //ballast factor, ballast-lamp photomateric factor, input watts
            //for absolute photometer: 1
            sb.AppendLine(String.Format("1 1 {0}", Wattage));
            
            //vertical values
            sb.AppendLine(String.Join(" ", vRange));

            //horizontal values
            sb.AppendLine(String.Join(" ", hRange));

            //raw values
            for (int h = 0; h < hRange.Length; h++)
            {
                string[] values = _data
                    .Where(m => m.Key == candleKey & m.Theta == hRange[h])
                    .Select(m => m.Value.ToString("0.##"))
                    .ToArray();

                sb.AppendLine(String.Join(" ", values));
            }

            return sb.ToString();
        }
    }
}
