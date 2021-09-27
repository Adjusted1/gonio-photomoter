namespace Goniometer
{
    partial class EditMotorView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param sensorname="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblVerticalMotorAngle = new System.Windows.Forms.Label();
            this.lblHorizontalMotorAngle = new System.Windows.Forms.Label();
            this.btnVerticalCW5 = new System.Windows.Forms.Button();
            this.btnVerticalCW1 = new System.Windows.Forms.Button();
            this.btnVerticalCW01 = new System.Windows.Forms.Button();
            this.btnVerticalCCW5 = new System.Windows.Forms.Button();
            this.btnVerticalCCW1 = new System.Windows.Forms.Button();
            this.btnVerticalCCW01 = new System.Windows.Forms.Button();
            this.btnHorizontalCCW01 = new System.Windows.Forms.Button();
            this.btnHorizontalCCW1 = new System.Windows.Forms.Button();
            this.btnHorizontalCCW5 = new System.Windows.Forms.Button();
            this.btnHorizontalCW01 = new System.Windows.Forms.Button();
            this.btnHorizontalCW1 = new System.Windows.Forms.Button();
            this.btnHorizontalCW5 = new System.Windows.Forms.Button();
            this.btnZeroVertical = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkZero = new System.Windows.Forms.CheckBox();
            this.lblHorizontalLoadAngle = new System.Windows.Forms.Label();
            this.lblVerticalLoadAngle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gaugeVertical = new System.Windows.Forms.AGauge();
            this.gaugeHorizontal = new System.Windows.Forms.AGauge();
            this.btnZeroHorizontal = new System.Windows.Forms.Button();
            this.cboSpeedVertical = new System.Windows.Forms.ComboBox();
            this.cboSpeedHorizontal = new System.Windows.Forms.ComboBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblVerticalMotorAngle
            // 
            this.lblVerticalMotorAngle.AutoSize = true;
            this.lblVerticalMotorAngle.Location = new System.Drawing.Point(129, 226);
            this.lblVerticalMotorAngle.Name = "lblVerticalMotorAngle";
            this.lblVerticalMotorAngle.Size = new System.Drawing.Size(34, 13);
            this.lblVerticalMotorAngle.TabIndex = 14;
            this.lblVerticalMotorAngle.Text = "0.000";
            // 
            // lblHorizontalMotorAngle
            // 
            this.lblHorizontalMotorAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHorizontalMotorAngle.AutoSize = true;
            this.lblHorizontalMotorAngle.Location = new System.Drawing.Point(129, 493);
            this.lblHorizontalMotorAngle.Name = "lblHorizontalMotorAngle";
            this.lblHorizontalMotorAngle.Size = new System.Drawing.Size(34, 13);
            this.lblHorizontalMotorAngle.TabIndex = 15;
            this.lblHorizontalMotorAngle.Text = "0.000";
            // 
            // btnVerticalCW5
            // 
            this.btnVerticalCW5.Enabled = false;
            this.btnVerticalCW5.Location = new System.Drawing.Point(226, 3);
            this.btnVerticalCW5.Name = "btnVerticalCW5";
            this.btnVerticalCW5.Size = new System.Drawing.Size(75, 23);
            this.btnVerticalCW5.TabIndex = 16;
            this.btnVerticalCW5.Text = "CW 5";
            this.btnVerticalCW5.UseVisualStyleBackColor = true;
            this.btnVerticalCW5.Click += new System.EventHandler(this.btnVerticalCW5_Click);
            // 
            // btnVerticalCW1
            // 
            this.btnVerticalCW1.Enabled = false;
            this.btnVerticalCW1.Location = new System.Drawing.Point(226, 32);
            this.btnVerticalCW1.Name = "btnVerticalCW1";
            this.btnVerticalCW1.Size = new System.Drawing.Size(75, 23);
            this.btnVerticalCW1.TabIndex = 17;
            this.btnVerticalCW1.Text = "CW 1";
            this.btnVerticalCW1.UseVisualStyleBackColor = true;
            this.btnVerticalCW1.Click += new System.EventHandler(this.btnVerticalCW1_Click);
            // 
            // btnVerticalCW01
            // 
            this.btnVerticalCW01.Enabled = false;
            this.btnVerticalCW01.Location = new System.Drawing.Point(226, 61);
            this.btnVerticalCW01.Name = "btnVerticalCW01";
            this.btnVerticalCW01.Size = new System.Drawing.Size(75, 23);
            this.btnVerticalCW01.TabIndex = 18;
            this.btnVerticalCW01.Text = "CW 0.1";
            this.btnVerticalCW01.UseVisualStyleBackColor = true;
            this.btnVerticalCW01.Click += new System.EventHandler(this.btnVerticalCW01_Click);
            // 
            // btnVerticalCCW5
            // 
            this.btnVerticalCCW5.Enabled = false;
            this.btnVerticalCCW5.Location = new System.Drawing.Point(226, 142);
            this.btnVerticalCCW5.Name = "btnVerticalCCW5";
            this.btnVerticalCCW5.Size = new System.Drawing.Size(75, 23);
            this.btnVerticalCCW5.TabIndex = 19;
            this.btnVerticalCCW5.Text = "CCW 5";
            this.btnVerticalCCW5.UseVisualStyleBackColor = true;
            this.btnVerticalCCW5.Click += new System.EventHandler(this.btnVerticalCCW5_Click);
            // 
            // btnVerticalCCW1
            // 
            this.btnVerticalCCW1.Enabled = false;
            this.btnVerticalCCW1.Location = new System.Drawing.Point(226, 171);
            this.btnVerticalCCW1.Name = "btnVerticalCCW1";
            this.btnVerticalCCW1.Size = new System.Drawing.Size(75, 23);
            this.btnVerticalCCW1.TabIndex = 20;
            this.btnVerticalCCW1.Text = "CCW1";
            this.btnVerticalCCW1.UseVisualStyleBackColor = true;
            this.btnVerticalCCW1.Click += new System.EventHandler(this.btnVerticalCCW1_Click);
            // 
            // btnVerticalCCW01
            // 
            this.btnVerticalCCW01.Enabled = false;
            this.btnVerticalCCW01.Location = new System.Drawing.Point(226, 200);
            this.btnVerticalCCW01.Name = "btnVerticalCCW01";
            this.btnVerticalCCW01.Size = new System.Drawing.Size(75, 23);
            this.btnVerticalCCW01.TabIndex = 21;
            this.btnVerticalCCW01.Text = "CCW 0.1";
            this.btnVerticalCCW01.UseVisualStyleBackColor = true;
            this.btnVerticalCCW01.Click += new System.EventHandler(this.btnVerticalCCW01_Click);
            // 
            // btnHorizontalCCW01
            // 
            this.btnHorizontalCCW01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHorizontalCCW01.Enabled = false;
            this.btnHorizontalCCW01.Location = new System.Drawing.Point(226, 467);
            this.btnHorizontalCCW01.Name = "btnHorizontalCCW01";
            this.btnHorizontalCCW01.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontalCCW01.TabIndex = 27;
            this.btnHorizontalCCW01.Text = "CCW 0.1";
            this.btnHorizontalCCW01.UseVisualStyleBackColor = true;
            this.btnHorizontalCCW01.Click += new System.EventHandler(this.btnHorizontalCCW01_Click);
            // 
            // btnHorizontalCCW1
            // 
            this.btnHorizontalCCW1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHorizontalCCW1.Enabled = false;
            this.btnHorizontalCCW1.Location = new System.Drawing.Point(226, 439);
            this.btnHorizontalCCW1.Name = "btnHorizontalCCW1";
            this.btnHorizontalCCW1.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontalCCW1.TabIndex = 26;
            this.btnHorizontalCCW1.Text = "CCW 1";
            this.btnHorizontalCCW1.UseVisualStyleBackColor = true;
            this.btnHorizontalCCW1.Click += new System.EventHandler(this.btnHorizontalCCW1_Click);
            // 
            // btnHorizontalCCW5
            // 
            this.btnHorizontalCCW5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHorizontalCCW5.Enabled = false;
            this.btnHorizontalCCW5.Location = new System.Drawing.Point(226, 410);
            this.btnHorizontalCCW5.Name = "btnHorizontalCCW5";
            this.btnHorizontalCCW5.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontalCCW5.TabIndex = 25;
            this.btnHorizontalCCW5.Text = "CCW 5";
            this.btnHorizontalCCW5.UseVisualStyleBackColor = true;
            this.btnHorizontalCCW5.Click += new System.EventHandler(this.btnHorizontalCCW5_Click);
            // 
            // btnHorizontalCW01
            // 
            this.btnHorizontalCW01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHorizontalCW01.Enabled = false;
            this.btnHorizontalCW01.Location = new System.Drawing.Point(226, 328);
            this.btnHorizontalCW01.Name = "btnHorizontalCW01";
            this.btnHorizontalCW01.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontalCW01.TabIndex = 24;
            this.btnHorizontalCW01.Text = "CW 0.1";
            this.btnHorizontalCW01.UseVisualStyleBackColor = true;
            this.btnHorizontalCW01.Click += new System.EventHandler(this.btnHorizontalCW01_Click);
            // 
            // btnHorizontalCW1
            // 
            this.btnHorizontalCW1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHorizontalCW1.Enabled = false;
            this.btnHorizontalCW1.Location = new System.Drawing.Point(226, 299);
            this.btnHorizontalCW1.Name = "btnHorizontalCW1";
            this.btnHorizontalCW1.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontalCW1.TabIndex = 23;
            this.btnHorizontalCW1.Text = "CW 1";
            this.btnHorizontalCW1.UseVisualStyleBackColor = true;
            this.btnHorizontalCW1.Click += new System.EventHandler(this.btnHorizontalCW1_Click);
            // 
            // btnHorizontalCW5
            // 
            this.btnHorizontalCW5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHorizontalCW5.Enabled = false;
            this.btnHorizontalCW5.Location = new System.Drawing.Point(226, 270);
            this.btnHorizontalCW5.Name = "btnHorizontalCW5";
            this.btnHorizontalCW5.Size = new System.Drawing.Size(75, 23);
            this.btnHorizontalCW5.TabIndex = 22;
            this.btnHorizontalCW5.Text = "CW 5";
            this.btnHorizontalCW5.UseVisualStyleBackColor = true;
            this.btnHorizontalCW5.Click += new System.EventHandler(this.btnHorizontalCW5_Click);
            // 
            // btnZeroVertical
            // 
            this.btnZeroVertical.Enabled = false;
            this.btnZeroVertical.Location = new System.Drawing.Point(226, 100);
            this.btnZeroVertical.Name = "btnZeroVertical";
            this.btnZeroVertical.Size = new System.Drawing.Size(75, 23);
            this.btnZeroVertical.TabIndex = 28;
            this.btnZeroVertical.Text = "Zero";
            this.btnZeroVertical.UseVisualStyleBackColor = true;
            this.btnZeroVertical.Click += new System.EventHandler(this.btnZeroVertical_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(315, 507);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 29;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkZero
            // 
            this.chkZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkZero.AutoSize = true;
            this.chkZero.Location = new System.Drawing.Point(312, 7);
            this.chkZero.Name = "chkZero";
            this.chkZero.Size = new System.Drawing.Size(78, 17);
            this.chkZero.TabIndex = 30;
            this.chkZero.Text = "Zero Mode";
            this.chkZero.UseVisualStyleBackColor = true;
            this.chkZero.CheckedChanged += new System.EventHandler(this.chkZero_CheckedChanged);
            // 
            // lblHorizontalLoadAngle
            // 
            this.lblHorizontalLoadAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHorizontalLoadAngle.AutoSize = true;
            this.lblHorizontalLoadAngle.Location = new System.Drawing.Point(129, 509);
            this.lblHorizontalLoadAngle.Name = "lblHorizontalLoadAngle";
            this.lblHorizontalLoadAngle.Size = new System.Drawing.Size(34, 13);
            this.lblHorizontalLoadAngle.TabIndex = 32;
            this.lblHorizontalLoadAngle.Text = "0.000";
            // 
            // lblVerticalLoadAngle
            // 
            this.lblVerticalLoadAngle.AutoSize = true;
            this.lblVerticalLoadAngle.Location = new System.Drawing.Point(129, 242);
            this.lblVerticalLoadAngle.Name = "lblVerticalLoadAngle";
            this.lblVerticalLoadAngle.Size = new System.Drawing.Size(34, 13);
            this.lblVerticalLoadAngle.TabIndex = 33;
            this.lblVerticalLoadAngle.Text = "0.000";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 493);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Motor Pos:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 509);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Load Pos:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Load Pos:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Motor Pos:";
            // 
            // gaugeVertical
            // 
            this.gaugeVertical.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeVertical.BaseArcRadius = 80;
            this.gaugeVertical.BaseArcStart = 90;
            this.gaugeVertical.BaseArcSweep = 180;
            this.gaugeVertical.BaseArcWidth = 2;
            this.gaugeVertical.Center = new System.Drawing.Point(110, 110);
            this.gaugeVertical.Location = new System.Drawing.Point(0, 3);
            this.gaugeVertical.MaxValue = 180F;
            this.gaugeVertical.MinValue = 0F;
            this.gaugeVertical.Name = "gaugeVertical";
            this.gaugeVertical.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Red;
            this.gaugeVertical.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeVertical.NeedleRadius = 80;
            this.gaugeVertical.NeedleType = System.Windows.Forms.NeedleType.Simple;
            this.gaugeVertical.NeedleWidth = 2;
            this.gaugeVertical.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeVertical.ScaleLinesInterInnerRadius = 73;
            this.gaugeVertical.ScaleLinesInterOuterRadius = 80;
            this.gaugeVertical.ScaleLinesInterWidth = 1;
            this.gaugeVertical.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeVertical.ScaleLinesMajorInnerRadius = 70;
            this.gaugeVertical.ScaleLinesMajorOuterRadius = 80;
            this.gaugeVertical.ScaleLinesMajorStepValue = 45F;
            this.gaugeVertical.ScaleLinesMajorWidth = 2;
            this.gaugeVertical.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gaugeVertical.ScaleLinesMinorInnerRadius = 75;
            this.gaugeVertical.ScaleLinesMinorOuterRadius = 80;
            this.gaugeVertical.ScaleLinesMinorTicks = 9;
            this.gaugeVertical.ScaleLinesMinorWidth = 1;
            this.gaugeVertical.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gaugeVertical.ScaleNumbersFormat = null;
            this.gaugeVertical.ScaleNumbersRadius = 95;
            this.gaugeVertical.ScaleNumbersRotation = 0;
            this.gaugeVertical.ScaleNumbersStartScaleLine = 1;
            this.gaugeVertical.ScaleNumbersStepScaleLines = 1;
            this.gaugeVertical.Size = new System.Drawing.Size(220, 220);
            this.gaugeVertical.TabIndex = 38;
            this.gaugeVertical.TabStop = false;
            this.gaugeVertical.Text = "aGauge";
            this.gaugeVertical.Value = 0F;
            // 
            // gaugeHorizontal
            // 
            this.gaugeHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gaugeHorizontal.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeHorizontal.BaseArcRadius = 80;
            this.gaugeHorizontal.BaseArcStart = 90;
            this.gaugeHorizontal.BaseArcSweep = 360;
            this.gaugeHorizontal.BaseArcWidth = 2;
            this.gaugeHorizontal.Center = new System.Drawing.Point(110, 110);
            this.gaugeHorizontal.Location = new System.Drawing.Point(0, 270);
            this.gaugeHorizontal.MaxValue = 360F;
            this.gaugeHorizontal.MinValue = 0F;
            this.gaugeHorizontal.Name = "gaugeHorizontal";
            this.gaugeHorizontal.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Red;
            this.gaugeHorizontal.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeHorizontal.NeedleRadius = 80;
            this.gaugeHorizontal.NeedleType = System.Windows.Forms.NeedleType.Simple;
            this.gaugeHorizontal.NeedleWidth = 2;
            this.gaugeHorizontal.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeHorizontal.ScaleLinesInterInnerRadius = 73;
            this.gaugeHorizontal.ScaleLinesInterOuterRadius = 80;
            this.gaugeHorizontal.ScaleLinesInterWidth = 1;
            this.gaugeHorizontal.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeHorizontal.ScaleLinesMajorInnerRadius = 70;
            this.gaugeHorizontal.ScaleLinesMajorOuterRadius = 80;
            this.gaugeHorizontal.ScaleLinesMajorStepValue = 45F;
            this.gaugeHorizontal.ScaleLinesMajorWidth = 2;
            this.gaugeHorizontal.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gaugeHorizontal.ScaleLinesMinorInnerRadius = 75;
            this.gaugeHorizontal.ScaleLinesMinorOuterRadius = 80;
            this.gaugeHorizontal.ScaleLinesMinorTicks = 9;
            this.gaugeHorizontal.ScaleLinesMinorWidth = 1;
            this.gaugeHorizontal.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gaugeHorizontal.ScaleNumbersFormat = null;
            this.gaugeHorizontal.ScaleNumbersRadius = 95;
            this.gaugeHorizontal.ScaleNumbersRotation = 0;
            this.gaugeHorizontal.ScaleNumbersStartScaleLine = 2;
            this.gaugeHorizontal.ScaleNumbersStepScaleLines = 1;
            this.gaugeHorizontal.Size = new System.Drawing.Size(220, 220);
            this.gaugeHorizontal.TabIndex = 39;
            this.gaugeHorizontal.TabStop = false;
            this.gaugeHorizontal.Text = "aGauge";
            this.gaugeHorizontal.Value = 0F;
            // 
            // btnZeroHorizontal
            // 
            this.btnZeroHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZeroHorizontal.Enabled = false;
            this.btnZeroHorizontal.Location = new System.Drawing.Point(226, 368);
            this.btnZeroHorizontal.Name = "btnZeroHorizontal";
            this.btnZeroHorizontal.Size = new System.Drawing.Size(75, 23);
            this.btnZeroHorizontal.TabIndex = 40;
            this.btnZeroHorizontal.Text = "Zero";
            this.btnZeroHorizontal.UseVisualStyleBackColor = true;
            this.btnZeroHorizontal.Click += new System.EventHandler(this.btnZeroHorizontal_Click);
            // 
            // cboSpeedVertical
            // 
            this.cboSpeedVertical.FormattingEnabled = true;
            this.cboSpeedVertical.Items.AddRange(new object[] {
            "Normal",
            "Slow",
            "Slowest"});
            this.cboSpeedVertical.Location = new System.Drawing.Point(180, 234);
            this.cboSpeedVertical.Name = "cboSpeedVertical";
            this.cboSpeedVertical.Size = new System.Drawing.Size(121, 21);
            this.cboSpeedVertical.TabIndex = 41;
            this.cboSpeedVertical.SelectedIndexChanged += new System.EventHandler(this.cboSpeedVertical_SelectedIndexChanged);
            // 
            // cboSpeedHorizontal
            // 
            this.cboSpeedHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSpeedHorizontal.FormattingEnabled = true;
            this.cboSpeedHorizontal.Items.AddRange(new object[] {
            "Normal",
            "Slow",
            "Slowest"});
            this.cboSpeedHorizontal.Location = new System.Drawing.Point(180, 501);
            this.cboSpeedHorizontal.Name = "cboSpeedHorizontal";
            this.cboSpeedHorizontal.Size = new System.Drawing.Size(121, 21);
            this.cboSpeedHorizontal.TabIndex = 42;
            this.cboSpeedHorizontal.SelectedIndexChanged += new System.EventHandler(this.cboSpeedHorizontal_SelectedIndexChanged);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MotorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboSpeedHorizontal);
            this.Controls.Add(this.cboSpeedVertical);
            this.Controls.Add(this.btnZeroHorizontal);
            this.Controls.Add(this.gaugeHorizontal);
            this.Controls.Add(this.gaugeVertical);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblVerticalLoadAngle);
            this.Controls.Add(this.lblHorizontalLoadAngle);
            this.Controls.Add(this.chkZero);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnZeroVertical);
            this.Controls.Add(this.btnHorizontalCCW01);
            this.Controls.Add(this.btnHorizontalCCW1);
            this.Controls.Add(this.btnHorizontalCCW5);
            this.Controls.Add(this.btnHorizontalCW01);
            this.Controls.Add(this.btnHorizontalCW1);
            this.Controls.Add(this.btnHorizontalCW5);
            this.Controls.Add(this.btnVerticalCCW01);
            this.Controls.Add(this.btnVerticalCCW1);
            this.Controls.Add(this.btnVerticalCCW5);
            this.Controls.Add(this.btnVerticalCW01);
            this.Controls.Add(this.btnVerticalCW1);
            this.Controls.Add(this.btnVerticalCW5);
            this.Controls.Add(this.lblHorizontalMotorAngle);
            this.Controls.Add(this.lblVerticalMotorAngle);
            this.MinimumSize = new System.Drawing.Size(393, 533);
            this.Name = "MotorView";
            this.Size = new System.Drawing.Size(393, 533);
            this.Load += new System.EventHandler(this.MotorView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVerticalMotorAngle;
        private System.Windows.Forms.Label lblHorizontalMotorAngle;
        private System.Windows.Forms.Button btnVerticalCW5;
        private System.Windows.Forms.Button btnVerticalCW1;
        private System.Windows.Forms.Button btnVerticalCW01;
        private System.Windows.Forms.Button btnVerticalCCW5;
        private System.Windows.Forms.Button btnVerticalCCW1;
        private System.Windows.Forms.Button btnVerticalCCW01;
        private System.Windows.Forms.Button btnHorizontalCCW01;
        private System.Windows.Forms.Button btnHorizontalCCW1;
        private System.Windows.Forms.Button btnHorizontalCCW5;
        private System.Windows.Forms.Button btnHorizontalCW01;
        private System.Windows.Forms.Button btnHorizontalCW1;
        private System.Windows.Forms.Button btnHorizontalCW5;
        private System.Windows.Forms.Button btnZeroVertical;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkZero;
        private System.Windows.Forms.Label lblHorizontalLoadAngle;
        private System.Windows.Forms.Label lblVerticalLoadAngle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.AGauge gaugeVertical;
        private System.Windows.Forms.AGauge gaugeHorizontal;
        private System.Windows.Forms.Button btnZeroHorizontal;
        private System.Windows.Forms.ComboBox cboSpeedVertical;
        private System.Windows.Forms.ComboBox cboSpeedHorizontal;
        private System.Windows.Forms.Timer timer;
    }
}

