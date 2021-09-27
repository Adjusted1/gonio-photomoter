namespace Goniometer.Workflows.Calibration
{
    partial class CalibrationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wizardPages1 = new WizardPages();
            this.tabSetup = new System.Windows.Forms.TabPage();
            this.tableLampDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblCertTech = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCertDate = new System.Windows.Forms.Label();
            this.lblCertBy = new System.Windows.Forms.Label();
            this.lblUsage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLumens = new System.Windows.Forms.Label();
            this.lblAmps = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLamps = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabProgress = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lumenTestProgressControl1 = new Goniometer.LumenTestProgressControl();
            this.tabCompletion = new System.Windows.Forms.TabPage();
            this.wizardPages1.SuspendLayout();
            this.tabSetup.SuspendLayout();
            this.tableLampDetails.SuspendLayout();
            this.tabProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardPages1
            // 
            this.wizardPages1.Controls.Add(this.tabSetup);
            this.wizardPages1.Controls.Add(this.tabProgress);
            this.wizardPages1.Controls.Add(this.tabCompletion);
            this.wizardPages1.Location = new System.Drawing.Point(4, 4);
            this.wizardPages1.Name = "wizardPages1";
            this.wizardPages1.SelectedIndex = 0;
            this.wizardPages1.Size = new System.Drawing.Size(721, 561);
            this.wizardPages1.TabIndex = 0;
            // 
            // tabSetup
            // 
            this.tabSetup.Controls.Add(this.tableLampDetails);
            this.tabSetup.Controls.Add(this.cboLamps);
            this.tabSetup.Controls.Add(this.btnBack);
            this.tabSetup.Controls.Add(this.btnStart);
            this.tabSetup.Location = new System.Drawing.Point(4, 22);
            this.tabSetup.Name = "tabSetup";
            this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetup.Size = new System.Drawing.Size(713, 535);
            this.tabSetup.TabIndex = 0;
            this.tabSetup.Text = "Setup";
            this.tabSetup.UseVisualStyleBackColor = true;
            // 
            // tableLampDetails
            // 
            this.tableLampDetails.ColumnCount = 2;
            this.tableLampDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLampDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLampDetails.Controls.Add(this.lblCertTech, 1, 5);
            this.tableLampDetails.Controls.Add(this.label10, 0, 5);
            this.tableLampDetails.Controls.Add(this.lblCertDate, 1, 4);
            this.tableLampDetails.Controls.Add(this.lblCertBy, 1, 3);
            this.tableLampDetails.Controls.Add(this.lblUsage, 1, 2);
            this.tableLampDetails.Controls.Add(this.label1, 0, 0);
            this.tableLampDetails.Controls.Add(this.label3, 0, 1);
            this.tableLampDetails.Controls.Add(this.lblLumens, 1, 0);
            this.tableLampDetails.Controls.Add(this.lblAmps, 1, 1);
            this.tableLampDetails.Controls.Add(this.label6, 0, 4);
            this.tableLampDetails.Controls.Add(this.label8, 0, 2);
            this.tableLampDetails.Controls.Add(this.label2, 0, 3);
            this.tableLampDetails.Location = new System.Drawing.Point(20, 53);
            this.tableLampDetails.Name = "tableLampDetails";
            this.tableLampDetails.RowCount = 6;
            this.tableLampDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLampDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLampDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLampDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLampDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLampDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLampDetails.Size = new System.Drawing.Size(200, 144);
            this.tableLampDetails.TabIndex = 6;
            // 
            // lblCertTech
            // 
            this.lblCertTech.AutoSize = true;
            this.lblCertTech.Location = new System.Drawing.Point(103, 100);
            this.lblCertTech.Name = "lblCertTech";
            this.lblCertTech.Size = new System.Drawing.Size(32, 13);
            this.lblCertTech.TabIndex = 14;
            this.lblCertTech.Text = "Tech";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Certification Tech";
            // 
            // lblCertDate
            // 
            this.lblCertDate.AutoSize = true;
            this.lblCertDate.Location = new System.Drawing.Point(103, 80);
            this.lblCertDate.Name = "lblCertDate";
            this.lblCertDate.Size = new System.Drawing.Size(53, 13);
            this.lblCertDate.TabIndex = 12;
            this.lblCertDate.Text = "1/1/1900";
            // 
            // lblCertBy
            // 
            this.lblCertBy.AutoSize = true;
            this.lblCertBy.Location = new System.Drawing.Point(103, 60);
            this.lblCertBy.Name = "lblCertBy";
            this.lblCertBy.Size = new System.Drawing.Size(35, 13);
            this.lblCertBy.TabIndex = 10;
            this.lblCertBy.Text = "Name";
            // 
            // lblUsage
            // 
            this.lblUsage.AutoSize = true;
            this.lblUsage.Location = new System.Drawing.Point(103, 40);
            this.lblUsage.Name = "lblUsage";
            this.lblUsage.Size = new System.Drawing.Size(34, 13);
            this.lblUsage.TabIndex = 8;
            this.lblUsage.Text = "00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lumens";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Amperage";
            // 
            // lblLumens
            // 
            this.lblLumens.AutoSize = true;
            this.lblLumens.Location = new System.Drawing.Point(103, 0);
            this.lblLumens.Name = "lblLumens";
            this.lblLumens.Size = new System.Drawing.Size(13, 13);
            this.lblLumens.TabIndex = 4;
            this.lblLumens.Text = "0";
            // 
            // lblAmps
            // 
            this.lblAmps.AutoSize = true;
            this.lblAmps.Location = new System.Drawing.Point(103, 20);
            this.lblAmps.Name = "lblAmps";
            this.lblAmps.Size = new System.Drawing.Size(13, 13);
            this.lblAmps.TabIndex = 6;
            this.lblAmps.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Certification Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Total Usage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Certification By";
            // 
            // cboLamps
            // 
            this.cboLamps.FormattingEnabled = true;
            this.cboLamps.Location = new System.Drawing.Point(20, 26);
            this.cboLamps.Name = "cboLamps";
            this.cboLamps.Size = new System.Drawing.Size(121, 21);
            this.cboLamps.TabIndex = 2;
            this.cboLamps.SelectedIndexChanged += new System.EventHandler(this.cboLamps_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(7, 505);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(632, 506);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabProgress
            // 
            this.tabProgress.Controls.Add(this.button2);
            this.tabProgress.Controls.Add(this.button1);
            this.tabProgress.Controls.Add(this.lumenTestProgressControl1);
            this.tabProgress.Location = new System.Drawing.Point(4, 22);
            this.tabProgress.Name = "tabProgress";
            this.tabProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tabProgress.Size = new System.Drawing.Size(713, 535);
            this.tabProgress.TabIndex = 1;
            this.tabProgress.Text = "Progress";
            this.tabProgress.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 505);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(631, 506);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lumenTestProgressControl1
            // 
            this.lumenTestProgressControl1.Location = new System.Drawing.Point(7, 7);
            this.lumenTestProgressControl1.MinimumSize = new System.Drawing.Size(270, 327);
            this.lumenTestProgressControl1.Name = "lumenTestProgressControl1";
            this.lumenTestProgressControl1.Size = new System.Drawing.Size(700, 468);
            this.lumenTestProgressControl1.TabIndex = 0;
            // 
            // tabCompletion
            // 
            this.tabCompletion.Location = new System.Drawing.Point(4, 22);
            this.tabCompletion.Name = "tabCompletion";
            this.tabCompletion.Size = new System.Drawing.Size(713, 535);
            this.tabCompletion.TabIndex = 2;
            this.tabCompletion.Text = "Completion";
            this.tabCompletion.UseVisualStyleBackColor = true;
            // 
            // CalibrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardPages1);
            this.Name = "CalibrationControl";
            this.Size = new System.Drawing.Size(728, 568);
            this.Load += new System.EventHandler(this.CalibrationControl_Load);
            this.wizardPages1.ResumeLayout(false);
            this.tabSetup.ResumeLayout(false);
            this.tableLampDetails.ResumeLayout(false);
            this.tableLampDetails.PerformLayout();
            this.tabProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WizardPages wizardPages1;
        private System.Windows.Forms.TabPage tabSetup;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TabPage tabProgress;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private LumenTestProgressControl lumenTestProgressControl1;
        private System.Windows.Forms.TabPage tabCompletion;
        private System.Windows.Forms.ComboBox cboLamps;
        private System.Windows.Forms.TableLayoutPanel tableLampDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLumens;
        private System.Windows.Forms.Label lblAmps;
        private System.Windows.Forms.Label lblCertTech;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCertDate;
        private System.Windows.Forms.Label lblCertBy;
        private System.Windows.Forms.Label lblUsage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
    }
}
