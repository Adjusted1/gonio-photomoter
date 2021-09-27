using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Goniometer.Functions;
using Goniometer.Settings;
using Goniometer_Controller.Motors;
using Goniometer_Controller.Sensors;
using Goniometer.Sensors;

namespace Goniometer
{
    public partial class LumenTestSetupControl : UserControl, INotifyPropertyChanged
    {
        public LumenTestSetupControl()
        {
            InitializeComponent();
        }

        private void LumenTestSetupControl_Load(object sender, EventArgs e)
        {            
            //setup default values
            SetCalibrationFactors();
            txtDataFolder.Text = FileFolderProvider.DefaultDataFolder;
            
            //setup sensor control
            listSensors.Items.Clear();
            SensorProvider.GetSensors().ToList().ForEach(s => listSensors.Items.Add(s.Name));

            //check all items
            for (int i = 0; i < listSensors.Items.Count; i++)
                listSensors.SetItemChecked(i, true);

            UpdateMeasurementsDelegate ud = updateMeasurementGrid;
            IAsyncResult ar = ud.BeginInvoke(measurementGridUpdated, null);
        }

        #region Test Values
        public IEnumerable<BaseSensor> GetSensors()
        {
            //filter by checked items
            string[] values = listSensors.CheckedItems.OfType<string>().ToArray();
            return SensorProvider.GetSensors().WhereIn(s => s.Name, values);
        }

        public string DataFolder
        {
            get 
            { 
                return String.Format("{0}/{1:yyyyMMdd} {2} {3}",txtDataFolder.Text, DateTime.Now, this.TestName, this.Model); 
            }
            set
            {
                txtDataFolder.Text = value;
            }
        }

        public bool EmailNotifications
        {
            get { return chkEmail.Checked; }
            set { chkEmail.Checked = value; }
        }

        public string Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public string OutputFormat
        {
            get
            {
                return cboOutputFormat.SelectedItem as string;
            }
            set
            {
                cboOutputFormat.SelectedItem = value;
            }
        }
        #endregion

        #region Lamp Information
        public string TestName
        {
            get { return txtTestName.Text; }
            set { txtTestName.Text = value; }
        }

        public string Manufacturer
        {
            get { return txtManufacturer.Text; }
            set { txtManufacturer.Text = value; }
        }

        public string NumberOfLamps
        {
            get { return txtNumberOfLamps.Text; }
            set { txtNumberOfLamps.Text = value; }
        } 

        public string Model
        {
            get { return txtModel.Text; }
            set { txtModel.Text = value; }
        }

        public string Wattage
        {
            get { return txtWattage.Text; }
            set { txtWattage.Text = value; }
        }

        public string OpeningLength
        {
            get { return txtLength.Text; }
            set { txtLength.Text = value; }
        }

        public string OpeningWidth
        {
            get { return txtWidth.Text; }
            set { txtWidth.Text = value; }
        }

        public string OpeningHeight
        {
            get { return txtHeight.Text; }
            set { txtHeight.Text = value; }
        }
        #endregion

        #region Calibration Values
        public double KCal
        {
            get 
            { 
                double value;
                if (Double.TryParse(txtKCal.Text, out value))
                    return value;
                else
                    return 1;
            }
        }

        public double KTheta
        {
            get
            {
                double value;
                if (Double.TryParse(txtKTheta.Text, out value))
                    return value;
                else
                    return 1;
            }
        }

        public double Distance
        {
            get
            {
                double value;
                if (Double.TryParse(txtDistance.Text, out value))
                    return value;
                else
                    return 1;
            }
        }
        #endregion

        #region Range Values
        public double HorizontalResolution
        {
            get
            {
                double value;
                if (Double.TryParse(txtHorizontalResolution.Text, out value))
                    return value;
                else
                    return -1;
            }
            set { txtHorizontalResolution.Text = String.Format("{0}", value); }
        }

        public double HorizontalStrayResolution
        {
            get
            {
                double value;
                if (Double.TryParse(txtHorizontalStrayResolution.Text, out value))
                    return value;
                else
                    return -1;
            }
            set { txtHorizontalStrayResolution.Text = String.Format("{0}", value); }
        }

        public double VerticalResolution
        {
            get
            {
                double value;
                if (Double.TryParse(txtVerticalResolution.Text, out value))
                    return value;
                else
                    return -1;
            }
            set { txtVerticalResolution.Text = String.Format("{0}", value); }
        }

        public double VerticalStrayResolution
        {
            get
            {
                double value;
                if (Double.TryParse(txtVerticalStrayResolution.Text, out value))
                    return value;
                else
                    return -1;
            }
            set { txtVerticalStrayResolution.Text = String.Format("{0}", value); }
        }

        public VerticalSymmetryEnum VerticalSymmetry
        {
            get
            {
                if (radVerticalTop.Checked)
                    return VerticalSymmetryEnum.TopOnly;
                else if (radVerticalBottom.Checked)
                    return VerticalSymmetryEnum.BottomOnly;

                return VerticalSymmetryEnum.Full;
            }
            set 
            {
                switch (value)
                {
                    case VerticalSymmetryEnum.TopOnly:    
                        radVerticalTop.Checked = true;    
                        break;
                    case VerticalSymmetryEnum.BottomOnly: 
                        radVerticalBottom.Checked = true; 
                        break;
                    case VerticalSymmetryEnum.Full:
                        radVerticalFull.Checked = true;
                        break;
                }
            }
        }

        public HorizontalSymmetryEnum HorizontalSymmetry
        {
            get
            {
                if (radHorizontalFull.Checked)
                    return HorizontalSymmetryEnum.Full;
                else if (radHorizontalHalf.Checked)
                    return HorizontalSymmetryEnum.Half;
                else if (radHorizontalQuarter.Checked)
                    return HorizontalSymmetryEnum.Quarter;
                    
                return HorizontalSymmetryEnum.Single;
            }
            set
            {
                switch (value)
                {
                    case HorizontalSymmetryEnum.Full:
                        radHorizontalFull.Checked = true;
                        break;
                    case HorizontalSymmetryEnum.Half:
                        radHorizontalHalf.Checked = true;
                        break;
                    case HorizontalSymmetryEnum.Quarter:
                        radHorizontalQuarter.Checked = true;
                        break;
                    case HorizontalSymmetryEnum.Single:
                        radHorizontalSingle.Checked = true;
                        break;
                }
            }
        }

        public double VerticalStartRange
        {
            get
            {
                double value;
                if (Double.TryParse(txtVerticalStartRange.Text, out value))
                    return value;
                else
                    return -1;
            }
            set { txtVerticalStartRange.Text = String.Format("{0}", value); }
        }

        public double VerticalStopRange
        {
            get
            {
                double value;
                if (Double.TryParse(txtVerticalStopRange.Text, out value))
                    return value;
                else
                    return -1;
            }
            set { txtVerticalStopRange.Text = String.Format("{0}", value); }
        }
        #endregion

        #region Range Calculations
        public double[] CalculateHorizontalRange()
        {
            return CalculateHorizontalRange(HorizontalResolution);
        }

        public double[] CalculateStrayHorizontalRange()
        {
            return CalculateHorizontalRange(HorizontalStrayResolution);
        }

        private double[] CalculateHorizontalRange(double resolution)
        {
            double hRange = 0;  //total range in degrees
            switch(HorizontalSymmetry)
            {
                case HorizontalSymmetryEnum.Full:
                    hRange = 360;
                    break;

                case HorizontalSymmetryEnum.Half:
                    hRange = 180;
                    break;

                case HorizontalSymmetryEnum.Quarter:
                    hRange = 90;
                    break;

                case HorizontalSymmetryEnum.Single:
                    return new double[] { 0 };
            }

            if (resolution <= 0 || resolution > hRange)
                return new double[] { 0, hRange };

            return ReportUtils.Range(resolution, 0, hRange);
        }

        public double[] CalculateVerticalRange()
        {
            return CalculateVerticalRange(VerticalResolution);
        }

        public double[] CalculateStrayVerticalRange()
        {
            return CalculateVerticalRange(VerticalStrayResolution);
        }
        private double[] CalculateVerticalRange(double resolution)
        {
            double vRangeStart = 0;     //start of range in degrees
            double vRangeStop = 180;     //stop of range in degrees

            if (VerticalSymmetry == VerticalSymmetryEnum.TopOnly)
                vRangeStart = 90;
            else if (VerticalSymmetry == VerticalSymmetryEnum.BottomOnly)
                vRangeStop = 90;

            //hand entered values take precident
            if (VerticalStartRange >= 0)
                vRangeStart = VerticalStartRange;

            if (VerticalStopRange >= 0)
                vRangeStop = VerticalStopRange;

            if (resolution <= 0 || resolution > vRangeStop - vRangeStart)
                return new double[] { vRangeStart, vRangeStop };

            return ReportUtils.Range(resolution, vRangeStart, vRangeStop);
        }
        #endregion

        #region resolution textboxes
        private void txtHorizontalResolution_TextChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("HorizontalResolution");
            UpdateEstimate();
        }

        private void txtVerticalResolution_TextChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalResolution");
            UpdateEstimate();
        }

        private void cboStrayResolution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtVerticalStrayResolution_TextChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("HorizontalStrayResolution");
        }

        private void txtHorizontalStrayResolution_TextChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalStrayResolution");
        }

        private void txtVerticalStart_TextChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalStartRange");
        }

        private void txtVerticalStop_TextChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalStopRange");
        }
        #endregion

        #region vertical radios
        private void radVerticalFull_CheckedChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalSymmetry");
            UpdateEstimate();
        }

        private void radVerticalTop_CheckedChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalSymmetry");
            UpdateEstimate();
        }

        private void radVerticalBottom_CheckedChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("VerticalSymmetry");
            UpdateEstimate();
        }
        #endregion

        #region horizontal radios
        private void radHorizontalFull_CheckedChanged(object sender, EventArgs e)
        {
            txtHorizontalResolution.Enabled = true;
            txtHorizontalStrayResolution.Enabled = true;

            NotifyPropertyChanged("HorizontalSymmetry");
            UpdateEstimate();
        }

        private void radHorizontalHalf_CheckedChanged(object sender, EventArgs e)
        {
            txtHorizontalResolution.Enabled = true;
            txtHorizontalStrayResolution.Enabled = true;

            NotifyPropertyChanged("HorizontalSymmetry");
            UpdateEstimate();
        }

        private void radHorizontalQuarter_CheckedChanged(object sender, EventArgs e)
        {
            txtHorizontalResolution.Enabled = true;
            txtHorizontalStrayResolution.Enabled = true;
            
            NotifyPropertyChanged("HorizontalSymmetry");
            UpdateEstimate();
        }

        private void radHorizontalSingle_CheckedChanged(object sender, EventArgs e)
        {
            txtHorizontalResolution.Enabled = false;
            txtHorizontalResolution.Text = String.Format("{0}", 0);

            txtHorizontalStrayResolution.Enabled = false;
            txtHorizontalStrayResolution.Text = String.Format("{0}", 0);
            
            NotifyPropertyChanged("HorizontalSymmetry");
            UpdateEstimate();
        }
        #endregion

        #region sensor timer
        private delegate void UpdateMeasurementsDelegate();

        private object _sensorLock = new object();

        private void listSensors_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateMeasurementsDelegate ud = updateMeasurementGrid;
            IAsyncResult ar = ud.BeginInvoke(measurementGridUpdated, null);
        }

        private void updateMeasurementGrid()
        {
            lock (_sensorLock)
            {
                try
                {
                    double theta = MotorController.GetHorizontalMotorPosition();
                    double phi = MotorController.GetVerticalMotorPosition();
                    double exactTheta = MotorController.GetHorizontalEncoderPosition();
                    double exactPhi = MotorController.GetVerticalEncoderPosition();

                    var sensors = this.GetSensors().ToList();
                    var measurements = sensors.AsParallel()
                        .SelectMany(s => s.CollectMeasurements(theta, phi, exactTheta, exactPhi))
                        .ToList();

                    measurementGridView.DataSource = measurements;
                }
                catch (Exception ex)
                {
                    SimpleLogger.Logging.WriteToLog(ex.Message);
                }
            }
        }

        private void measurementGridUpdated(IAsyncResult result)
        {
        }
        #endregion

        #region Calibration Factors
        private void txtKCal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var view = new CalibrationView("txtKCal"))
            {
                var result = view.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SetCalibrationFactors();
                }
            }
        }

        private void txtKTheta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var view = new CalibrationView("txtKTheta"))
            {
                var result = view.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SetCalibrationFactors();
                }
            }
        }

        private void txtDistance_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var view = new CalibrationView("txtDistance"))
            {
                var result = view.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SetCalibrationFactors();
                }
            }
        }

        private void SetCalibrationFactors()
        {
            txtKCal.Text     = String.Format("{0:0.####}", CalibrationModel.KCal);
            txtKTheta.Text   = String.Format("{0:0.####}", CalibrationModel.KTheta);
            txtDistance.Text = String.Format("{0:0.####}", CalibrationModel.Distance);
        } 
        #endregion

        private void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEmail.Enabled = chkEmail.Checked;
        }

        private void btnDataFolder_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtDataFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

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
        #endregion

        /// <summary>
        /// set the estimate label based on horizontal and vertical steps
        /// </summary>
        private void UpdateEstimate()
        {
            lblTime.Text = "00:00:00";

            //regular test
            double[] hRange = CalculateHorizontalRange();
            double[] vRange = CalculateVerticalRange();
            TimeSpan test = ReportUtils.TimeEstimate(hRange.Length, vRange.Length);

            //stray test
            double[] hStrayRange = CalculateStrayHorizontalRange();
            double[] vStrayRange = CalculateStrayVerticalRange();
            TimeSpan stray = ReportUtils.TimeEstimate(hStrayRange.Length, vStrayRange.Length);

            var result = test + stray;

            lblTime.Text = String.Format("{0:hh\\:mm\\:ss}", result);
        }

        /// <summary>
        /// Check if setup values are valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            var sensors = this.GetSensors();
            if (sensors.Count() == 0)
                return false;

            if (sensors.First() == null)
                return false;

            if (String.IsNullOrEmpty(txtDataFolder.Text))
                return false;

            if (String.IsNullOrEmpty(txtTestName.Text))
                return false;

            if (String.IsNullOrEmpty(txtManufacturer.Text))
                return false;

            if (String.IsNullOrEmpty(txtModel.Text))
                return false;

            if (this.HorizontalResolution < 0)
                return false;

            if (this.VerticalResolution < 0)
                return false;

            if (this.HorizontalStrayResolution < 0)
                return false;

            if (this.VerticalStrayResolution < 0)
                return false;

            return true;
        }

        private bool _readOnly = false;
        public void SetReadOnly(bool readOnly)
        {
            this._readOnly = readOnly;

            //data folder controls
            this.txtDataFolder.ReadOnly = readOnly;
            this.btnDataFolder.Enabled  = readOnly;

            //text boxes
            this.txtNumberOfLamps.ReadOnly = readOnly;
            this.txtManufacturer.ReadOnly  = readOnly;
            this.txtTestName.ReadOnly      = readOnly;
            this.txtModel.ReadOnly         = readOnly;
            this.txtWidth.ReadOnly         = readOnly;
            this.txtHeight.ReadOnly        = readOnly;
            this.txtLength.ReadOnly        = readOnly;
            this.txtWattage.ReadOnly       = readOnly;
            
            //vertical range controls
            this.txtVerticalResolution.ReadOnly      = readOnly;
            this.txtVerticalStrayResolution.ReadOnly = readOnly;
            this.txtVerticalStopRange.ReadOnly       = readOnly;
            this.txtVerticalStartRange.ReadOnly      = readOnly;

            this.radVerticalFull.Enabled   = readOnly;
            this.radVerticalBottom.Enabled = readOnly;
            this.radVerticalTop.Enabled    = readOnly;

            //horizontal range controls
            this.txtHorizontalResolution.ReadOnly      = readOnly;
            this.txtHorizontalStrayResolution.ReadOnly = readOnly;

            this.radHorizontalFull.Enabled    = readOnly;
            this.radHorizontalQuarter.Enabled = readOnly;
            this.radHorizontalHalf.Enabled    = readOnly;
            this.radHorizontalSingle.Enabled  = readOnly;

            this.cboStrayResolution.Enabled = readOnly;

            //email controls
            //ignore as these can be set
            //this.txtEmail.ReadOnly = readOnly;
            //this.chkEmail.ReadOnly = readOnly;

            //calibration controls
            //this.txtDistance.ReadOnly = readOnly;
            //this.txtKTheta.ReadOnly   = readOnly;
            //this.txtKCal.ReadOnly     = readOnly;
        }
    }
}