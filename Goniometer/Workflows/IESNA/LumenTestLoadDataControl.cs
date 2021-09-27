using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Goniometer_Controller.Models;

namespace Goniometer.Workflows.IESNA
{
    public partial class LumenTestLoadDataControl : UserControl
    {
        public LumenTestLoadDataControl()
        {
            InitializeComponent();
        }

        public LumenTestSettingsModel FoundSettings { get; private set; }
        public MeasurementCollection FoundLightData { get; private set; }
        public MeasurementCollection FoundStrayData { get; private set; }

        public bool LightTestFinished = false;
        public bool StrayTestFinished = false;

        private void btnDataFolder_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtDataFolder.Text = folderBrowserDialog.SelectedPath;

                if (Directory.Exists(txtDataFolder.Text))
                {
                    var files = Directory.EnumerateFiles(txtDataFolder.Text);

                    //find settings files
                    var settingsFileName = files.FirstOrDefault(f => f.EndsWith("settings.xml"));
                    if (!String.IsNullOrEmpty(settingsFileName))
                    {
                        //produce settings file
                        FoundSettings = LumenTestSettingsModel.ReadXML(settingsFileName);

                        //TODO: validate settings file
                        if (FoundSettings.VerticalStartRange < 0)
                        {
                            FoundSettings.VerticalStartRange = FoundSettings.VerticalSymmetry.StartAngle();
                        }

                        if (FoundSettings.VerticalStopRange < 0)
                        {
                            FoundSettings.VerticalStopRange = FoundSettings.VerticalSymmetry.StopAngle();
                        }

                    }
                    else
                    {
                        //return with error
                        return;
                    }

                    //find raw files
                    var rawLightFileName = files.FirstOrDefault(f => f.EndsWith("raw_light.csv"));
                    if (!String.IsNullOrEmpty(rawLightFileName))
                    {
                        //produce datafile
                        FoundLightData = MeasurementCollection.FromCSVFile(rawLightFileName);


                        //did we finish light test?
                        //look for last value, see if it exists
                        var lastLightPoint = FoundLightData.FirstOrDefault(m =>
                            m.Theta == (int)FoundSettings.HorizontalSymmetry
                            && m.Phi == FoundSettings.VerticalStopRange);

                        if (lastLightPoint == null)
                        {
                            //light test not finished
                            //find the biggest phi that was completed
                            double bestVStart = FoundLightData
                                .Where(m => m.Theta == (int)FoundSettings.HorizontalSymmetry)
                                .Max(m => m.Phi);
                            
                            //stop off incomplete portions
                            var invalidLightPoints = FoundLightData.Where(m => m.Phi >= bestVStart).ToList();
                            invalidLightPoints.ForEach(p => FoundLightData.Remove(p));

                            //update to newest start value
                            FoundSettings.VerticalStartRange = bestVStart;
                        }
                        else
                        {
                            //light test finished
                            LightTestFinished = true;
                        }

                        //find raw stray files
                        var rawStrayFileName = files.FirstOrDefault(f => f.EndsWith("raw_stray.csv"));
                        if (!String.IsNullOrEmpty(rawStrayFileName))
                        {
                            //produce datafile
                            FoundStrayData = MeasurementCollection.FromCSVFile(rawStrayFileName);

                            //did we finish the stray test?
                            var lastStrayPoint = FoundStrayData.FirstOrDefault(m =>
                            m.Theta == (int)FoundSettings.HorizontalSymmetry
                            && m.Phi == FoundSettings.VerticalStopRange);

                            if (lastStrayPoint == null)
                            {
                                //stray test not finished
                                //find the biggest phi that was completed
                                double bestVStart = FoundStrayData
                                    .Where(m => m.Theta == (int)FoundSettings.HorizontalSymmetry)
                                    .Max(m => m.Phi);

                                //stop off incomplete portions
                                var invalidStrayPoints = FoundStrayData.Where(m => m.Phi >= bestVStart).ToList();
                                invalidStrayPoints.ForEach(p => FoundStrayData.Remove(p));

                                //update to newest start value
                                FoundSettings.VerticalStartRange = bestVStart;
                            }
                            else
                            {
                                //stray test finished
                                StrayTestFinished = true;
                            }
                        }
                        else
                        {
                            //no stray file found
                            FoundStrayData = new MeasurementCollection();
                        }
                    }
                    else
                    {
                        // no light file found
                        FoundLightData = new MeasurementCollection();
                    }
                }
            }
        }
    }
}
