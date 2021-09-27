using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Goniometer.Functions;

using Goniometer_Controller;
using Goniometer_Controller.Models;
using Goniometer_Controller.Motors;
using Goniometer_Controller.Sensors;

namespace Goniometer.Workflows.IESNA
{
    public partial class LumenTestControl : UserControl, INotifyPropertyChanged
    {
        private bool _continueRun;

        /// <summary>
        /// create new lumen test control
        /// </summary>
        /// <param name="loadData">should we present the loaddata control?</param>
        public LumenTestControl(bool loadData)
        {
            InitializeComponent();

            this._continueRun = loadData;
            if (loadData)
            {
                setupControl.SetReadOnly(true);
                wizard.SelectedTab = tabLoadData;
            }
            else
            {
                //setupControl.SetReadOnly(false);
                wizard.SelectedTab = tabSetup;
            }
        }   

        private void LumenTestControl_Load(object sender, EventArgs e)
        {
            setupControl.PropertyChanged += new PropertyChangedEventHandler(setupControl_PropertyChanged);
            progressControl.TestCompleted += new EventHandler(progressControl_TestCompleted);
        }

        #region loaddata page
        private void btnExit_Click(object sender, EventArgs e)
        {
            var notify = OnExit;
            if (notify != null)
                notify(sender, e);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //check for valid settings file
            if (lumenTestLoadDataControl.FoundSettings == null)
                return;

            setupControl.EmailNotifications        = lumenTestLoadDataControl.FoundSettings.EmailNotifications;
            setupControl.Email                     = lumenTestLoadDataControl.FoundSettings.Email;
            setupControl.DataFolder                = lumenTestLoadDataControl.FoundSettings.DataFolder;
            setupControl.OutputFormat              = lumenTestLoadDataControl.FoundSettings.OutputFormat;
            setupControl.TestName                  = lumenTestLoadDataControl.FoundSettings.TestName;
            setupControl.Manufacturer              = lumenTestLoadDataControl.FoundSettings.Manufacturer;
            setupControl.Model                     = lumenTestLoadDataControl.FoundSettings.Model;
            setupControl.Wattage                   = lumenTestLoadDataControl.FoundSettings.Wattage;
            setupControl.OpeningLength             = lumenTestLoadDataControl.FoundSettings.OpeningLength;
            setupControl.OpeningWidth              = lumenTestLoadDataControl.FoundSettings.OpeningWidth;
            setupControl.OpeningHeight             = lumenTestLoadDataControl.FoundSettings.OpeningHeight;

            setupControl.HorizontalResolution      = lumenTestLoadDataControl.FoundSettings.HorizontalResolution;
            setupControl.HorizontalStrayResolution = lumenTestLoadDataControl.FoundSettings.HorizontalStrayResolution;
            setupControl.HorizontalSymmetry        = lumenTestLoadDataControl.FoundSettings.HorizontalSymmetry;
            setupControl.VerticalResolution        = lumenTestLoadDataControl.FoundSettings.VerticalResolution;
            setupControl.VerticalStrayResolution   = lumenTestLoadDataControl.FoundSettings.VerticalStrayResolution;
            setupControl.VerticalStartRange        = lumenTestLoadDataControl.FoundSettings.VerticalStartRange;
            setupControl.VerticalStopRange         = lumenTestLoadDataControl.FoundSettings.VerticalStopRange;
            setupControl.VerticalSymmetry          = lumenTestLoadDataControl.FoundSettings.VerticalSymmetry;

            //setupControl.KCal                      = lumenTestLoadDataControl.FoundSettings.kCal;
            //setupControl.KTheta                    = lumenTestLoadDataControl.FoundSettings.kTheta;
            //setupControl.Distance                  = lumenTestLoadDataControl.FoundSettings.distance;

            wizard.SelectedTab = tabSetup;
        }
        #endregion

        #region setup page
        private void btnBack_Click(object sender, EventArgs e)
        {
            var notify = OnExit;
            if (notify != null)
                notify(sender, e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!setupControl.IsValid())
                return;

            //pass values to other tab
            progressControl.EmailNotifications = setupControl.EmailNotifications;
            progressControl.Email              = setupControl.Email;
            progressControl.DataFolder         = setupControl.DataFolder;
            progressControl.OutputFormat       = setupControl.OutputFormat;

            //iesna report values
            progressControl.TestName           = setupControl.TestName;
            progressControl.Manufacturer       = setupControl.Manufacturer;
            progressControl.Model              = setupControl.Model;
            progressControl.Wattage            = setupControl.Wattage;
            progressControl.OpeningLength      = setupControl.OpeningLength;
            progressControl.OpeningWidth       = setupControl.OpeningWidth;
            progressControl.OpeningHeight      = setupControl.OpeningHeight;

            //running values
            double[] hRange                    = setupControl.CalculateHorizontalRange();
            double[] vRange                    = setupControl.CalculateVerticalRange();
            double[] hStrayRange               = setupControl.CalculateStrayHorizontalRange();
            double[] vStrayRange               = setupControl.CalculateStrayVerticalRange();

            double kCal                        = setupControl.KCal;
            double kTheta                      = setupControl.KTheta;
            double distance                    = setupControl.Distance;
            
            //fetch a list of active sensors
            var sensors = setupControl.GetSensors();
            
            //data containers
            MeasurementCollection rawLightData;
            MeasurementCollection rawStrayData;

            if (!this._continueRun)
            {
                //create settings file for new run
                LumenTestSettingsModel settings = new LumenTestSettingsModel
                {
                    EmailNotifications = setupControl.EmailNotifications,
                    Email = setupControl.Email,
                    DataFolder = setupControl.DataFolder,
                    TestName = setupControl.TestName,
                    Manufacturer = setupControl.Manufacturer,
                    Model = setupControl.Model,
                    Wattage = setupControl.Wattage,
                    OpeningLength = setupControl.OpeningLength,
                    OpeningWidth = setupControl.OpeningWidth,
                    OpeningHeight = setupControl.OpeningHeight,
                    HorizontalResolution = setupControl.HorizontalResolution,
                    HorizontalStrayResolution = setupControl.HorizontalStrayResolution,
                    HorizontalSymmetry = setupControl.HorizontalSymmetry,
                    VerticalResolution = setupControl.VerticalResolution,
                    VerticalStrayResolution = setupControl.VerticalStrayResolution,
                    VerticalStartRange = setupControl.VerticalStartRange,
                    VerticalStopRange = setupControl.VerticalStopRange,
                    VerticalSymmetry = setupControl.VerticalSymmetry,
                    kCal = setupControl.KCal,
                    kTheta = setupControl.KTheta,
                    distance = setupControl.Distance,
                };

                //save settings to file
                LumenTestSettingsModel.WriteXML(settings, setupControl.DataFolder + "\\settings.xml");

                //create new data containers
                rawLightData = new MeasurementCollection();
                rawStrayData = new MeasurementCollection();
            }
            else
            {
                //fix data folder setting
                progressControl.DataFolder = lumenTestLoadDataControl.FoundSettings.DataFolder;

                //fetch data
                rawLightData = lumenTestLoadDataControl.FoundLightData;
                rawStrayData = lumenTestLoadDataControl.FoundStrayData;

                //pass skip values
                progressControl.SkipLightTest = lumenTestLoadDataControl.LightTestFinished;
                progressControl.SkipStrayTest = lumenTestLoadDataControl.StrayTestFinished;
            }

            //start test
            progressControl.BeginTestAsync(sensors, hRange, vRange, hStrayRange, vStrayRange, kCal, kTheta, distance, rawLightData, rawStrayData);
            wizard.SelectedTab = tabProgress;
        }

        #region setup values
        public double HorizontalResolution
        {
            get { return setupControl.HorizontalResolution; }
        }

        public double HorizontalStrayResolution
        {
            get { return setupControl.HorizontalStrayResolution; }
        }

        public double VerticalResolution
        {
            get { return setupControl.VerticalResolution; }
        }

        public double VerticalStrayResolution
        {
            get { return setupControl.VerticalStrayResolution; }
        }

        public VerticalSymmetryEnum VerticalSymmetry
        {
            get { return setupControl.VerticalSymmetry; }
        }

        public HorizontalSymmetryEnum HorizontalSymmetry
        {
            get { return setupControl.HorizontalSymmetry; }
        }
        #endregion
        #endregion

        #region progresss page
        private void btnCancel_Click(object sender, EventArgs e)
        {
            progressControl.CancelTestAsync();

            //pass values to other tab
            setupControl.EmailNotifications = progressControl.EmailNotifications;
            setupControl.Email              = progressControl.Email;

            wizard.SelectedTab = tabCompletion;
        }

        private bool paused = false;
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!paused)
            {
                btnPause.Text = "Unpause";
                paused = true;
                progressControl.PauseTestAsync();
            }
            else
            {
                btnPause.Text = "Pause";
                paused = false;
                progressControl.UnpauseTestAsync();
            }
        }

        private void progressControl_TestCompleted(object sender, EventArgs e)
        {
            wizard.SelectedTab = tabCompletion;

            //this is where report generation should occur
        }
        #endregion

        #region completion page
        /// <summary>
        /// Called when the control signals that it is ready to be closed
        /// </summary>
        public event EventHandler OnExit;
        private void btnClose_Click(object sender, EventArgs e)
        {
            var notify = OnExit;
            if (notify != null)
                notify(sender, e);
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var notify = PropertyChanged;
            if (notify != null)
            {
                notify(this, new PropertyChangedEventArgs(info));
            }
        }

        private void setupControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var notify = PropertyChanged;
            if (notify != null)
            {
                notify(sender, e);
            }
        }
        #endregion
    }
}
