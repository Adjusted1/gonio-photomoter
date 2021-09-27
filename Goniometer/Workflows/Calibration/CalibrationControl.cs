using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Goniometer_Controller.Calibration;
using Goniometer_Controller.Calibration.CalibrationDataSetTableAdapters;

namespace Goniometer.Workflows.Calibration
{
    public partial class CalibrationControl : UserControl
    {
        TableAdapterManager manager = new TableAdapterManager();
        CalibrationDataSet dataset = new CalibrationDataSet();

        public CalibrationControl()
        {
            InitializeComponent();

            //read out data from database
            manager.FillDataSet(dataset);
        }

        private void CalibrationControl_Load(object sender, EventArgs e)
        {
            cboLamps.DataSource = dataset.Lamps.DefaultView;
            cboLamps.ValueMember = "ID";
            cboLamps.DisplayMember = "Name";
        }

        #region setup tab
        private void cboLamps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLamps.SelectedItem == null)
                return;

            var lamp = (CalibrationDataSet.LampsRow)((DataRowView)cboLamps.SelectedItem).Row;

            //certification info
            var cert = lamp.GetCertificationsRows().FirstOrDefault();
            if (cert != null)
            {
                lblLumens.Text = String.Format("{0:#}", cert.Lumens);
                lblAmps.Text = String.Format("{0:0.0}", cert.Amperage);
                lblCertBy.Text = cert.Certification_By;
                lblCertDate.Text = String.Format("{0:MM/dd/yyyy}", cert.Certification_Date);
                lblCertTech.Text = cert.Certification_Technician;
            }
            else
            {
                lblLumens.Text = "Unknown";
                lblAmps.Text = "Unknown";
                lblCertBy.Text = "Unknown";
                lblCertDate.Text = "Unknown";
                lblCertTech.Text = "Unknown";
            }

            //usage info
            TimeSpan? usage = lamp.GetUsage_LogRows()
                    .Select(u => new TimeSpan(u.Duration.Hour, u.Duration.Minute, u.Duration.Second)).Sum();
            if (usage.HasValue)
                lblUsage.Text = String.Format("{0:hh\\:mm}", usage.Value);
            else
                lblUsage.Text = "00:00";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        } 
        #endregion
    }
}
