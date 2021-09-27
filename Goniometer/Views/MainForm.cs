using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Goniometer.Functions;
using Goniometer.Reports;
using Goniometer.Sensors;
using Goniometer.Workflows;
using Goniometer.Workflows.IESNA;
using Goniometer_Controller;
using Goniometer_Controller.Models;
using Goniometer_Controller.Motors;
using Goniometer_Controller.Sensors;

namespace Goniometer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // test email subsystem
            ReportUtils.EmailResults("main gonio gui load completed", "see subj", "sweetlawrence205@gmail.com", null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                //connect to motor
                string address = ConfigurationManager.AppSettings["motor.ip"];
                MotorController.Connect(IPAddress.Parse(address));

                //connect to sensors
                SensorProvider.LoadSensorConfiguration();
            }
            catch (Exception)
            {
                //controller failed to connect
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MotorController.EmergencyStop();
        }

        #region menu items

        #region file
        private void openRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(openFileDialog.FileName);
                    if (!fi.Exists)
                        return;

                    LoadDataControl(fi.FullName);
                }
            }
            catch (Exception)
            { 
            }
        }
        #endregion

        #region workflows
        /// <summary>
        /// starts new lumen test workflow
        /// </summary>
        private void newLumenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLumenTestControl(false);
        }

        /// <summary>
        /// restarts lumen test workflow from raw file
        /// </summary>
        private void continueLumenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLumenTestControl(true);
        }
        #endregion

        #region tools
        /// <summary>
        /// launch raw file merge tool
        /// </summary>
        private void mergeRawFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// launch raw file view tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewRawFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region settings
        private void motorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMotorSettingsControl();
        }

        private void sensorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #endregion

        #region main panel context switching

        #region Settings Control
        private void LoadMotorSettingsControl()
        {
            panelMain.Controls.Clear();

            //initialize and attach view
            var editMotorControl = new EditMotorView();
            editMotorControl.Size = panelMain.Size;
            editMotorControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            editMotorControl.OnCloseClicked += motorSettingsControl_OnCloseClicked;

            panelMain.Controls.Add(editMotorControl);
        }

        private void motorSettingsControl_OnCloseClicked(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
        }
        #endregion

        #region LumenTestControl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadData">should we present the loaddata control?</param>
        private void LoadLumenTestControl(bool loadData)
        {
            panelMain.Controls.Clear();

            //initialize and attach view
            var lumenTestControl = new LumenTestControl(loadData);
            lumenTestControl.Size = panelMain.Size;
            lumenTestControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lumenTestControl.PropertyChanged += new PropertyChangedEventHandler(lumenTestControl_PropertyChanged);
            lumenTestControl.OnExit += new EventHandler(lumenTestControl_OnExit);

            panelMain.Controls.Add(lumenTestControl);
        }

        private void lumenTestControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                LumenTestSetupControl control = sender as LumenTestSetupControl;
                if (control == null)
                    return;

                if (e.PropertyName == "HorizontalResolution" | e.PropertyName == "HorizontalSymmetry")
                {
                    double[] hRange = control.CalculateHorizontalRange();
                    if (hRange.Length == 0)
                        return;

                    motorControlHorizontal.GaugeRangeStartValue = Convert.ToSingle(hRange.First());
                    motorControlHorizontal.GaugeRangeEndValue   = Convert.ToSingle(hRange.Last());
                }
                else if (e.PropertyName == "VerticalResolution"
                       | e.PropertyName == "VerticalSymmetry"
                       | e.PropertyName == "VerticalStartRange"
                       | e.PropertyName == "VerticalStopRange")
                {
                    double[] vRange = control.CalculateVerticalRange();
                    if (vRange.Length == 0)
                        return;

                    motorControlVertical.GaugeRangeStartValue = Convert.ToSingle(vRange.First());
                    motorControlVertical.GaugeRangeEndValue   = Convert.ToSingle(vRange.Last());
                }
            }
            catch
            {
                //omnomnom
            }
        }

        private void lumenTestControl_OnExit(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
        }
        #endregion

        #region DataControl
        private void LoadDataControl(string filePath)
        {
            panelMain.Controls.Clear();

            //initialize and attach view
            var dataControl = new RawDataView();
            dataControl.Size = panelMain.Size;
            dataControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            panelMain.Controls.Add(dataControl);

            //fetch data and bind it
            using (StreamReader sr = new StreamReader(filePath))
            {
                string raw = sr.ReadToEnd();
                var measurements = MeasurementCollection.FromCSV(raw);
                dataControl.BindDataSource(measurements);
            }
        }
        #endregion

        #endregion

        private void timerMotor_Tick(object sender, EventArgs e)
        {
            try
            {
                //check motor controller
                MotorController.CheckErrorStatus();

                //check motor controller enabled status
                MotorController.CheckMotorEnabled();

                //check motor position
                motorControlHorizontal.GaugeAngle = Convert.ToSingle(MotorController.GetHorizontalEncoderPosition());
                motorControlVertical.GaugeAngle   = Convert.ToSingle(MotorController.GetVerticalEncoderPosition());
            }
            catch (Exception ex)
            {
                SimpleLogger.Logging.WriteToLog(ex.Message);
            }
        }

        private void btnPanic_Click(object sender, EventArgs e)
        {
            MotorController.EmergencyStop();
        }

        private void motorControlVertical_OnButtonGoClicked(object sender, double? e)
        {
            if (e.HasValue)
            {
                ExecuteSetVerticalAngleDelegate d = ExecuteSetVerticalAngle;
                IAsyncResult ar = d.BeginInvoke(e.Value, SetVerticalAngleFinished, null);
            }
        }

        private delegate void ExecuteSetVerticalAngleDelegate(double angle);
        private void ExecuteSetVerticalAngle(double angle)
        {
            try
            {
                MotorController.SetVerticalAngle(angle);
            }
            catch (InvalidOperationException ex)
            {
                SimpleLogger.Logging.WriteToLog(ex.Message);
            }
        }

        private void SetVerticalAngleFinished(IAsyncResult result)
        {
            //?
        }

        private void motorControlHorizontal_OnButtonGoClicked(object sender, double? e)
        {
            if (e.HasValue)
            {
                ExecuteSetHorizontalAngleDelegate d = ExecuteSetHorizontalAngle;
                IAsyncResult ar = d.BeginInvoke(e.Value, SetHorizontalAngleFinished, null);
            }
        }

        private delegate void ExecuteSetHorizontalAngleDelegate(double angle);
        private void ExecuteSetHorizontalAngle(double angle)
        {
            try
            {
                MotorController.SetHorizontalAngleAndWait(angle);
            }
            catch (InvalidOperationException ex)
            {
                SimpleLogger.Logging.WriteToLog(ex.Message);
            }
        }

        private void SetHorizontalAngleFinished(IAsyncResult result)
        {
            //?
        }
    }
}
