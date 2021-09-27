using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Goniometer.Settings
{
    public partial class CalibrationView : Form
    {
        private string _controlSelect;

        public CalibrationView()
        {
            InitializeComponent();
        }

        public CalibrationView(string controlSelect)
        {
            InitializeComponent();
            this._controlSelect = controlSelect;
        }

        private void CalibrationView_Load(object sender, EventArgs e)
        {
            txtKCal.Text     = String.Format("{0:0.####}", CalibrationModel.KCal);
            txtKTheta.Text   = String.Format("{0:0.####}", CalibrationModel.KTheta);
            txtDistance.Text = String.Format("{0:0.####}", CalibrationModel.Distance);

            if (!String.IsNullOrEmpty(_controlSelect))
            {
                try
                {
                    var control = this.Controls[_controlSelect];
                    if (control != null)
                        control.Select();
                }
                catch (Exception) { /*omnomnom*/ }
            }

        }

        private void txtKCal_TextChanged(object sender, EventArgs e)
        {
            ValidateValues();
        }

        private void txtKTheta_TextChanged(object sender, EventArgs e)
        {
            ValidateValues();
        }

        private void txtDistance_TextChanged(object sender, EventArgs e)
        {
            ValidateValues();
        }

        private bool ValidateValues()
        {
            bool validates = true;

            double kCal;
            if (Double.TryParse(txtKCal.Text, out kCal))
            {
                //value must be positive nonzero
                if (kCal <= 0)
                    validates = false;
            }
            else
            {
                validates = false;
            }

            double kTheta;
            if (Double.TryParse(txtKTheta.Text, out kTheta))
            {
                //value must be positive nonzero
                if (kTheta <= 0)
                    validates = false;
            }
            else
            {
                validates = false;
            }

            double distance;
            if (Double.TryParse(txtDistance.Text, out distance))
            {
                //value must be positive nonzero
                if (distance <= 0)
                    validates = false;
            }
            else
            {
                validates = false;
            }

            return validates;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                CalibrationModel.KCal     = Double.Parse(txtKCal.Text);
                CalibrationModel.KTheta   = Double.Parse(txtKTheta.Text);
                CalibrationModel.Distance = Double.Parse(txtDistance.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
