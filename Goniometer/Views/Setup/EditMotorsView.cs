using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

using Goniometer_Controller.Motors;

namespace Goniometer
{
    public partial class EditMotorView : UserControl
    {
        public EditMotorView()
        {
            InitializeComponent();
        }

        protected void MotorView_Load(object sender, EventArgs e)
        {
            SetZeroingMode(false);
            timer.Enabled = true;
        }

        #region speed dropdowns
        private void cboSpeedVertical_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cboSpeedVertical.SelectedItem.ToString();

            if (item.Equals("NORMAL", StringComparison.CurrentCultureIgnoreCase))
            {
                MotorController.VerticalSpeed = MotionSpeed.Normal;
            }
            else if (item.Equals("SLOW", StringComparison.CurrentCultureIgnoreCase))
            {
                MotorController.VerticalSpeed = MotionSpeed.Slow;
            }
            else if (item.Equals("SLOWEST", StringComparison.CurrentCultureIgnoreCase))
            {
                MotorController.VerticalSpeed = MotionSpeed.Slowest;
            }
        }

        private void cboSpeedHorizontal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cboSpeedHorizontal.SelectedItem.ToString();

            if (item.Equals("NORMAL", StringComparison.CurrentCultureIgnoreCase))
            {
                MotorController.HorizontalSpeed = MotionSpeed.Normal;
            }
            else if (item.Equals("SLOW", StringComparison.CurrentCultureIgnoreCase))
            {
                MotorController.HorizontalSpeed = MotionSpeed.Slow;
            }
            else if (item.Equals("SLOWEST", StringComparison.CurrentCultureIgnoreCase))
            {
                MotorController.HorizontalSpeed = MotionSpeed.Slowest;
            }
        }
        #endregion

        #region zeroing buttons
        private void btnVerticalCW5_Click(object sender, EventArgs e)
        {
            MoveVertical(5);
        }

        private void btnVerticalCW1_Click(object sender, EventArgs e)
        {
            MoveVertical(1);
        }

        private void btnVerticalCW01_Click(object sender, EventArgs e)
        {
            MoveVertical(0.1);
        }

        private void btnVerticalCCW5_Click(object sender, EventArgs e)
        {
            MoveVertical(-5);
        }

        private void btnVerticalCCW1_Click(object sender, EventArgs e)
        {
            MoveVertical(-1);
        }

        private void btnVerticalCCW01_Click(object sender, EventArgs e)
        {
            MoveVertical(-0.1);
        }

        private void MoveVertical(double distance)
        {
            MotorController.SetVerticalAngle(distance);
        }

        private void btnHorizontalCW5_Click(object sender, EventArgs e)
        {
            MoveHorizontal(5);
        }

        private void btnHorizontalCW1_Click(object sender, EventArgs e)
        {
            MoveHorizontal(1);
        }

        private void btnHorizontalCW01_Click(object sender, EventArgs e)
        {
            MoveHorizontal(0.1);
        }

        private void btnHorizontalCCW5_Click(object sender, EventArgs e)
        {
            MoveHorizontal(-5);
        }

        private void btnHorizontalCCW1_Click(object sender, EventArgs e)
        {
            MoveHorizontal(-1);
        }

        private void btnHorizontalCCW01_Click(object sender, EventArgs e)
        {
            MoveHorizontal(-0.1);
        }

        private void MoveHorizontal(double distance)
        {
            MotorController.SetHorizontalAngle(distance);
        }
        #endregion

        #region zeroing methods
        private void btnZeroVertical_Click(object sender, EventArgs e)
        {
            MotorController.ZeroVerticalMotor();
        }

        private void btnZeroHorizontal_Click(object sender, EventArgs e)
        {
            MotorController.ZeroHorizontalMotor();
        }

        private void chkZero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZero.Checked)
                SetZeroingMode(true);
            else
                SetZeroingMode(false);
        }

        private void SetZeroingMode(bool zeroing)
        {
            if (zeroing)
            {
                try
                {
                    MotorController.EnterZeroingMode();
                }
                catch (InvalidOperationException)
                {
                    //omnomnom, communication error
                }

                btnVerticalCW5.Enabled = true;
                btnVerticalCW1.Enabled = true;
                btnVerticalCW01.Enabled = true;
                btnVerticalCCW5.Enabled = true;
                btnVerticalCCW1.Enabled = true;
                btnVerticalCCW01.Enabled = true;

                btnHorizontalCW5.Enabled = true;
                btnHorizontalCW1.Enabled = true;
                btnHorizontalCW01.Enabled = true;
                btnHorizontalCCW5.Enabled = true;
                btnHorizontalCCW1.Enabled = true;
                btnHorizontalCCW01.Enabled = true;

                btnZeroVertical.Enabled = true;
                btnZeroHorizontal.Enabled = true;
            }
            else
            {
                try
                {
                    MotorController.ExitZeroingMode();
                }
                catch (InvalidOperationException)
                {
                    //omnomnom, communications error
                }

                btnVerticalCW5.Enabled = false;
                btnVerticalCW1.Enabled = false;
                btnVerticalCW01.Enabled = false;
                btnVerticalCCW5.Enabled = false;
                btnVerticalCCW1.Enabled = false;
                btnVerticalCCW01.Enabled = false;

                btnHorizontalCW5.Enabled = false;
                btnHorizontalCW1.Enabled = false;
                btnHorizontalCW01.Enabled = false;
                btnHorizontalCCW5.Enabled = false;
                btnHorizontalCCW1.Enabled = false;
                btnHorizontalCCW01.Enabled = false;

                btnZeroVertical.Enabled = false;
                btnZeroHorizontal.Enabled = false;
            }
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            gaugeHorizontal.Value = Convert.ToSingle(MotorController.GetHorizontalEncoderPosition());
            gaugeVertical.Value   = Convert.ToSingle(MotorController.GetVerticalEncoderPosition());

            lblHorizontalMotorAngle.Text = String.Format("{0:0.0}", MotorController.GetHorizontalMotorPosition());
            lblHorizontalLoadAngle.Text  = String.Format("{0:0.0}", MotorController.GetHorizontalEncoderPosition());
            lblVerticalMotorAngle.Text   = String.Format("{0:0.0}", MotorController.GetVerticalMotorPosition());
            lblVerticalLoadAngle.Text    = String.Format("{0:0.0}", MotorController.GetVerticalEncoderPosition());
        }

        public event EventHandler OnCloseClicked;
        private void btnExit_Click(object sender, EventArgs e)
        {
            SetZeroingMode(false);

            var tmp = OnCloseClicked;
            if (tmp != null)
                tmp(sender, e);
        }

    }
}
