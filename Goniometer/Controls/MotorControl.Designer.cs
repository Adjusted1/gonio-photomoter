namespace Goniometer.Controls
{
    partial class MotorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.AGaugeRange aGaugeRange1 = new System.Windows.Forms.AGaugeRange();
            this.gauge = new System.Windows.Forms.AGauge();
            this.btnGo = new System.Windows.Forms.Button();
            this.lblAngle = new System.Windows.Forms.Label();
            this.txtAngle = new Goniometer.Controls.NumberTextBox();
            this.SuspendLayout();
            // 
            // gauge
            // 
            this.gauge.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge.BaseArcRadius = 80;
            this.gauge.BaseArcStart = 90;
            this.gauge.BaseArcSweep = 180;
            this.gauge.BaseArcWidth = 2;
            this.gauge.Center = new System.Drawing.Point(110, 110);
            aGaugeRange1.Color = System.Drawing.Color.Green;
            aGaugeRange1.EndValue = 180F;
            aGaugeRange1.InnerRadius = 75;
            aGaugeRange1.InRange = false;
            aGaugeRange1.Name = "GaugeRange";
            aGaugeRange1.OuterRadius = 80;
            aGaugeRange1.StartValue = 0F;
            this.gauge.GaugeRanges.Add(aGaugeRange1);
            this.gauge.Location = new System.Drawing.Point(0, 0);
            this.gauge.MaxValue = 180F;
            this.gauge.MinValue = 0F;
            this.gauge.Name = "gauge";
            this.gauge.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Red;
            this.gauge.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gauge.NeedleRadius = 80;
            this.gauge.NeedleType = System.Windows.Forms.NeedleType.Simple;
            this.gauge.NeedleWidth = 2;
            this.gauge.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge.ScaleLinesInterInnerRadius = 73;
            this.gauge.ScaleLinesInterOuterRadius = 80;
            this.gauge.ScaleLinesInterWidth = 1;
            this.gauge.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge.ScaleLinesMajorInnerRadius = 70;
            this.gauge.ScaleLinesMajorOuterRadius = 80;
            this.gauge.ScaleLinesMajorStepValue = 45F;
            this.gauge.ScaleLinesMajorWidth = 2;
            this.gauge.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge.ScaleLinesMinorInnerRadius = 75;
            this.gauge.ScaleLinesMinorOuterRadius = 80;
            this.gauge.ScaleLinesMinorTicks = 9;
            this.gauge.ScaleLinesMinorWidth = 1;
            this.gauge.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge.ScaleNumbersFormat = null;
            this.gauge.ScaleNumbersRadius = 95;
            this.gauge.ScaleNumbersRotation = 0;
            this.gauge.ScaleNumbersStartScaleLine = 1;
            this.gauge.ScaleNumbersStepScaleLines = 1;
            this.gauge.Size = new System.Drawing.Size(220, 220);
            this.gauge.TabIndex = 1;
            this.gauge.TabStop = false;
            this.gauge.Text = "aGauge";
            this.gauge.Value = 0F;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(178, 232);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(39, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(15, 237);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(25, 13);
            this.lblAngle.TabIndex = 2;
            this.lblAngle.Text = "360";
            this.lblAngle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(57, 234);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(104, 20);
            this.txtAngle.TabIndex = 3;
            this.txtAngle.Text = "0";
            this.txtAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAngle.Value = 0D;
            this.txtAngle.TextChanged += new System.EventHandler(this.txtAngle_TextChanged);
            // 
            // MotorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAngle);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblAngle);
            this.Controls.Add(this.gauge);
            this.Name = "MotorControl";
            this.Size = new System.Drawing.Size(220, 258);
            this.Load += new System.EventHandler(this.MotorControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.AGauge gauge;
        private NumberTextBox txtAngle;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblAngle;
    }
}
