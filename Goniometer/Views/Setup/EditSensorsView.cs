using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Goniometer_Controller;
using Goniometer_Controller.Sensors;
using Goniometer_Controller.Models;
using Goniometer.Sensors;
using Minolta_Controller;

namespace Goniometer.Setup
{
    public partial class EditSensorsView : UserControl
    {
        private BaseSensor _sensor;

        public EditSensorsView()
        {
            InitializeComponent();
        }

        private void SensorSetup_Load(object sender, EventArgs e)
        {
            ResetControl();
        }

        #region reset methods
        private void ResetControl()
        {
            if (_sensor != null)
            {
                _sensor.Dispose();
                _sensor = null;
            }

            lblMessage.Text = "Pick a Sensor";

            RefreshPortList();
            RefreshSensorList();
            ResetSensorsList();
        }

        private void RefreshSensorList()
        {
            cboSensor.SelectedItem = null;
            cboSensor.Items.Clear();
            cboSensor.Items.AddRange(MinoltaSensorFactory.GetSensorTypes().ToArray());
        }

        private void RefreshPortList()
        {
            cboPort.SelectedItem = null;
            cboPort.Items.Clear();
            cboPort.Items.AddRange(SerialPort.GetPortNames());
        } 

        private void ResetSensorsList()
        {
            listSensors.Items.Clear();
            SensorProvider.GetSensors().ToList().ForEach(s => listSensors.Items.Add(s.Name));
        }
        #endregion

        #region add methods
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSensor();
        }

        private void AddSensor()
        {
            _sensor.Name = txtName.Text;

            SensorProvider.AddSensor(_sensor);
            ResetSensorsList();
        }
        #endregion

        #region remove methods

        #endregion

        private void cboSensor_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;

            if (cboSensor.SelectedItem == null || String.IsNullOrEmpty(cboSensor.SelectedItem.ToString()))
            {
                //warn user
                lblMessage.Text = "Pick a Sensor";
                cboPort.Enabled = false;
            }
            else
            {
                lblMessage.Text = "Pick a Port";
                cboPort.Enabled = true;
            }
        }

        private void cboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;

            //check portName
            if (cboPort.SelectedItem == null || String.IsNullOrEmpty(cboPort.SelectedItem.ToString()))
                return;

            //start connection process
            var connectAsync = new ConnectAsyncDelegate(Connect);
            connectAsync.Invoke();
        }

        private delegate void ConnectAsyncDelegate();
        private void Connect()
        {
            try
            {
                //disable controls
                cboSensor.Enabled = false;
                cboPort.Enabled = false;
                btnAdd.Enabled = false;

                //clear out current readings
                measurementGridView.DataSource = null;

                //check sensorName
                if (cboSensor.SelectedItem == null || String.IsNullOrEmpty(cboSensor.SelectedItem.ToString()))
                    return;

                //check portName
                if (cboPort.SelectedItem == null || String.IsNullOrEmpty(cboPort.SelectedItem.ToString()))
                    return;

                lblMessage.Text = "Connecting";

                //prepare sensor in local variable before assigning to member
                var port = SerialPortProvider.GetPort(cboPort.SelectedItem.ToString());
                _sensor = MinoltaSensorFactory.CreateSensor("", cboSensor.SelectedItem.ToString(), port);

                //test the read method on the sensor
                var measurements = _sensor.CollectMeasurements(0, 0, 0, 0);
                measurementGridView.DataSource = measurements;

                lblMessage.Text = "Success";
                btnAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = String.Format("Error. Wrong Type/Port?\n{0}", ex.Message);
            }
            finally
            {
                //re-enable controls
                cboSensor.Enabled = true;
                cboPort.Enabled = true;
                btnAdd.Enabled = true;
            }
        }
    }
}
