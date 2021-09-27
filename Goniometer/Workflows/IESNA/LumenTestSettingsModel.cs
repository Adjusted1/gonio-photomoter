using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Goniometer.Workflows.IESNA
{
    public class LumenTestSettingsModel
    {
        public bool EmailNotifications;
        public string Email;
        public string DataFolder;
        public string OutputFormat;

        //iesna report values
        public string TestName;
        public string Manufacturer;
        public string Model;
        public string Wattage;
        public string OpeningLength;
        public string OpeningWidth;
        public string OpeningHeight;
        
        //running values
        public double HorizontalResolution;
        public double HorizontalStrayResolution;
        public HorizontalSymmetryEnum HorizontalSymmetry;

        public double VerticalResolution;
        public double VerticalStrayResolution;
        public double VerticalStartRange;
        public double VerticalStopRange;
        public VerticalSymmetryEnum VerticalSymmetry;
        
        //calibration values
        public double kCal;
        public double kTheta;
        public double distance;

        public static void WriteXML(LumenTestSettingsModel settings, string filename)
        {
            FileInfo fi = new FileInfo(filename);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            XmlSerializer writer = new XmlSerializer(typeof(LumenTestSettingsModel));
            using (var file = new System.IO.StreamWriter(filename))
            {
                writer.Serialize(file, settings);
                file.Close();
            }
        }

        public static LumenTestSettingsModel ReadXML(string filename)
        {
            XmlSerializer reader = new XmlSerializer(typeof(LumenTestSettingsModel));
            using (var file = new System.IO.StreamReader(filename))
            {
                LumenTestSettingsModel settings = new LumenTestSettingsModel();
                settings = (LumenTestSettingsModel)reader.Deserialize(file);
                file.Close();

                return settings;
            }
        }
    }
}