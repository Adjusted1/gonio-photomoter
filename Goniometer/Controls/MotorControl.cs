using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Goniometer.Controls
{
    public partial class MotorControl : UserControl
    {
        #region Properties
        [Browsable(true)]
        [Category("AGauge")]
        [Description("The maximum value to show on the scale.")]
        public int MaxGaugeAngle
        {
            get { return gauge.BaseArcSweep; }
            set
            {
                gauge.MaxValue = value;
                gauge.BaseArcSweep = value;

                //adjust for full circle
                if (value >= 360)
                    gauge.ScaleNumbersStartScaleLine = 2;
                else
                    gauge.ScaleNumbersStartScaleLine = 1;
            }
        }
        [Browsable(true)]
        [Category("AGauge")]
        [Description("Gauge value.")]
        public float GaugeAngle
        {
            get { return gauge.Value; }
            set
            {
                gauge.Value = value;
                lblAngle.Text = String.Format("{0:0}", value);
            }
        }

        public float GaugeRangeStartValue
        {
            get { return gauge.GaugeRanges[0].StartValue; }
            set { gauge.GaugeRanges[0].StartValue = value; }
        }

        public float GaugeRangeEndValue
        {
            get { return gauge.GaugeRanges[0].EndValue; }
            set { gauge.GaugeRanges[0].EndValue = value; }
        }

        public double? TextBoxValue
        {
            get { return txtAngle.Value; }
            set { txtAngle.Value = value; }
        }
        #endregion

        #region Constructor
        public MotorControl()
        {
            InitializeComponent();
        }
        #endregion

        private void MotorControl_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler<double?> OnButtonGoClicked;
        private void btnGo_Click(object sender, EventArgs e)
        {
            var temp = OnButtonGoClicked;
            if (temp != null)
                temp(sender, TextBoxValue);
        }

        #region TextBox Validations
        public event EventHandler OnTextAngleChanged;
        private void txtAngle_TextChanged(object sender, EventArgs e)
        {
            if (txtAngle.Value > MaxGaugeAngle)
            {
                //invalid entry
                btnGo.Enabled = false;
            }
            else
            {
                btnGo.Enabled = true;
            }

            var temp = OnTextAngleChanged;
            if (temp != null)
                temp(sender, e);
        }
        #endregion
    }
}
