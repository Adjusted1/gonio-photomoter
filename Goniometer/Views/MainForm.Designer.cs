namespace Goniometer
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workflowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLumenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueLumenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeRawFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRawFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPanic = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.motorControlHorizontal = new Goniometer.Controls.MotorControl();
            this.motorControlVertical = new Goniometer.Controls.MotorControl();
            this.panelMain = new System.Windows.Forms.Panel();
            this.timerMotor = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.workflowsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1471, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRawToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openRawToolStripMenuItem
            // 
            this.openRawToolStripMenuItem.Name = "openRawToolStripMenuItem";
            this.openRawToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.openRawToolStripMenuItem.Text = "Open Raw";
            this.openRawToolStripMenuItem.Click += new System.EventHandler(this.openRawToolStripMenuItem_Click);
            // 
            // workflowsToolStripMenuItem
            // 
            this.workflowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLumenTestToolStripMenuItem,
            this.continueLumenTestToolStripMenuItem});
            this.workflowsToolStripMenuItem.Name = "workflowsToolStripMenuItem";
            this.workflowsToolStripMenuItem.Size = new System.Drawing.Size(91, 24);
            this.workflowsToolStripMenuItem.Text = "Workflows";
            // 
            // newLumenTestToolStripMenuItem
            // 
            this.newLumenTestToolStripMenuItem.Name = "newLumenTestToolStripMenuItem";
            this.newLumenTestToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.newLumenTestToolStripMenuItem.Text = "New Lumen Test";
            this.newLumenTestToolStripMenuItem.Click += new System.EventHandler(this.newLumenTestToolStripMenuItem_Click);
            // 
            // continueLumenTestToolStripMenuItem
            // 
            this.continueLumenTestToolStripMenuItem.Name = "continueLumenTestToolStripMenuItem";
            this.continueLumenTestToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.continueLumenTestToolStripMenuItem.Text = "Continue Lumen Test";
            this.continueLumenTestToolStripMenuItem.Click += new System.EventHandler(this.continueLumenTestToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeRawFilesToolStripMenuItem,
            this.viewRawFilesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mergeRawFilesToolStripMenuItem
            // 
            this.mergeRawFilesToolStripMenuItem.Name = "mergeRawFilesToolStripMenuItem";
            this.mergeRawFilesToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.mergeRawFilesToolStripMenuItem.Text = "Merge Raw Files";
            this.mergeRawFilesToolStripMenuItem.Click += new System.EventHandler(this.mergeRawFilesToolStripMenuItem_Click);
            // 
            // viewRawFilesToolStripMenuItem
            // 
            this.viewRawFilesToolStripMenuItem.Name = "viewRawFilesToolStripMenuItem";
            this.viewRawFilesToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.viewRawFilesToolStripMenuItem.Text = "View Raw Files";
            this.viewRawFilesToolStripMenuItem.Click += new System.EventHandler(this.viewRawFilesToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motorToolStripMenuItem,
            this.sensorToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // motorToolStripMenuItem
            // 
            this.motorToolStripMenuItem.Name = "motorToolStripMenuItem";
            this.motorToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.motorToolStripMenuItem.Text = "Motor";
            this.motorToolStripMenuItem.Click += new System.EventHandler(this.motorToolStripMenuItem_Click);
            // 
            // sensorToolStripMenuItem
            // 
            this.sensorToolStripMenuItem.Name = "sensorToolStripMenuItem";
            this.sensorToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.sensorToolStripMenuItem.Text = "Sensor";
            this.sensorToolStripMenuItem.Click += new System.EventHandler(this.sensorToolStripMenuItem_Click);
            // 
            // btnPanic
            // 
            this.btnPanic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPanic.BackColor = System.Drawing.Color.Red;
            this.btnPanic.Location = new System.Drawing.Point(16, 34);
            this.btnPanic.Margin = new System.Windows.Forms.Padding(4);
            this.btnPanic.Name = "btnPanic";
            this.btnPanic.Size = new System.Drawing.Size(117, 831);
            this.btnPanic.TabIndex = 1;
            this.btnPanic.TabStop = false;
            this.btnPanic.Text = "STOP";
            this.btnPanic.UseVisualStyleBackColor = false;
            this.btnPanic.Click += new System.EventHandler(this.btnPanic_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelStatus.Controls.Add(this.motorControlHorizontal);
            this.panelStatus.Controls.Add(this.motorControlVertical);
            this.panelStatus.Location = new System.Drawing.Point(1175, 30);
            this.panelStatus.Margin = new System.Windows.Forms.Padding(4);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(295, 835);
            this.panelStatus.TabIndex = 2;
            // 
            // motorControlHorizontal
            // 
            this.motorControlHorizontal.GaugeAngle = 0F;
            this.motorControlHorizontal.GaugeRangeEndValue = 180F;
            this.motorControlHorizontal.GaugeRangeStartValue = 0F;
            this.motorControlHorizontal.Location = new System.Drawing.Point(0, 322);
            this.motorControlHorizontal.Margin = new System.Windows.Forms.Padding(5);
            this.motorControlHorizontal.MaxGaugeAngle = 360;
            this.motorControlHorizontal.Name = "motorControlHorizontal";
            this.motorControlHorizontal.Size = new System.Drawing.Size(293, 318);
            this.motorControlHorizontal.TabIndex = 11;
            this.motorControlHorizontal.TextBoxValue = 0D;
            this.motorControlHorizontal.OnButtonGoClicked += new System.EventHandler<System.Nullable<double>>(this.motorControlHorizontal_OnButtonGoClicked);
            // 
            // motorControlVertical
            // 
            this.motorControlVertical.GaugeAngle = 0F;
            this.motorControlVertical.GaugeRangeEndValue = 180F;
            this.motorControlVertical.GaugeRangeStartValue = 0F;
            this.motorControlVertical.Location = new System.Drawing.Point(0, 0);
            this.motorControlVertical.Margin = new System.Windows.Forms.Padding(5);
            this.motorControlVertical.MaxGaugeAngle = 180;
            this.motorControlVertical.Name = "motorControlVertical";
            this.motorControlVertical.Size = new System.Drawing.Size(293, 318);
            this.motorControlVertical.TabIndex = 10;
            this.motorControlVertical.TextBoxValue = 0D;
            this.motorControlVertical.OnButtonGoClicked += new System.EventHandler<System.Nullable<double>>(this.motorControlVertical_OnButtonGoClicked);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMain.Location = new System.Drawing.Point(141, 31);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1027, 834);
            this.panelMain.TabIndex = 2;
            // 
            // timerMotor
            // 
            this.timerMotor.Enabled = true;
            this.timerMotor.Tick += new System.EventHandler(this.timerMotor_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "csv";
            this.openFileDialog.FileName = "raw.csv";
            this.openFileDialog.Filter = "All files|*.*|CSV Files|*.csv";
            this.openFileDialog.FilterIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 880);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.btnPanic);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1262, 707);
            this.Name = "MainForm";
            this.Text = "Goniometer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem motorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorToolStripMenuItem;
        private System.Windows.Forms.Button btnPanic;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Timer timerMotor;
        private System.Windows.Forms.ToolStripMenuItem openRawToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Controls.MotorControl motorControlHorizontal;
        private Controls.MotorControl motorControlVertical;
        private System.Windows.Forms.ToolStripMenuItem workflowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLumenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueLumenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeRawFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewRawFilesToolStripMenuItem;
    }
}