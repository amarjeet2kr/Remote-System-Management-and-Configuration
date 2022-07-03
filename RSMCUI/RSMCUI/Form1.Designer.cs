namespace RSMCUI
{
    partial class RSMCUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ClientGrid = new System.Windows.Forms.DataGridView();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hostname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AntiVirusStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirewallStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobStatusMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ButtonInstall = new System.Windows.Forms.Button();
            this.ButtonUninstall = new System.Windows.Forms.Button();
            this.ButtonCopyFile = new System.Windows.Forms.Button();
            this.ButtonGetInfo = new System.Windows.Forms.Button();
            this.ButtonAbout = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonHelp = new System.Windows.Forms.Button();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonSettings = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonGetInfoOutput = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ClientGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClientGrid
            // 
            this.ClientGrid.AllowUserToAddRows = false;
            this.ClientGrid.AllowUserToDeleteRows = false;
            this.ClientGrid.AllowUserToOrderColumns = true;
            this.ClientGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ClientGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.ClientGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ClientGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ClientGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.Hostname,
            this.os,
            this.OSVersion,
            this.AntiVirusStatus,
            this.FirewallStatus,
            this.JobName,
            this.JobStatus,
            this.JobStatusMsg});
            this.ClientGrid.Location = new System.Drawing.Point(2, 90);
            this.ClientGrid.Name = "ClientGrid";
            this.ClientGrid.ReadOnly = true;
            this.ClientGrid.RowHeadersWidth = 20;
            this.ClientGrid.Size = new System.Drawing.Size(1135, 515);
            this.ClientGrid.TabIndex = 11;
            this.ClientGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientGrid_RowEnter);
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.Width = 80;
            // 
            // Hostname
            // 
            this.Hostname.HeaderText = "Host Name";
            this.Hostname.Name = "Hostname";
            this.Hostname.ReadOnly = true;
            this.Hostname.Width = 120;
            // 
            // os
            // 
            this.os.HeaderText = "Operating System";
            this.os.Name = "os";
            this.os.ReadOnly = true;
            this.os.Width = 150;
            // 
            // OSVersion
            // 
            this.OSVersion.HeaderText = "Operating System Version";
            this.OSVersion.Name = "OSVersion";
            this.OSVersion.ReadOnly = true;
            this.OSVersion.Width = 150;
            // 
            // AntiVirusStatus
            // 
            this.AntiVirusStatus.HeaderText = "Antivirus Status";
            this.AntiVirusStatus.Name = "AntiVirusStatus";
            this.AntiVirusStatus.ReadOnly = true;
            // 
            // FirewallStatus
            // 
            this.FirewallStatus.HeaderText = "Firewall Status";
            this.FirewallStatus.Name = "FirewallStatus";
            this.FirewallStatus.ReadOnly = true;
            // 
            // JobName
            // 
            this.JobName.HeaderText = "Job Name";
            this.JobName.Name = "JobName";
            this.JobName.ReadOnly = true;
            // 
            // JobStatus
            // 
            this.JobStatus.HeaderText = "Job Status";
            this.JobStatus.Name = "JobStatus";
            this.JobStatus.ReadOnly = true;
            // 
            // JobStatusMsg
            // 
            this.JobStatusMsg.HeaderText = "Status Message";
            this.JobStatusMsg.Name = "JobStatusMsg";
            this.JobStatusMsg.ReadOnly = true;
            this.JobStatusMsg.Width = 196;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Client Machines:";
            // 
            // StatusStrip
            // 
            this.StatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.StatusStrip.Location = new System.Drawing.Point(0, 623);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1139, 22);
            this.StatusStrip.TabIndex = 12;
            this.StatusStrip.Text = "Status:";
            // 
            // ButtonInstall
            // 
            this.ButtonInstall.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.ButtonInstall.Location = new System.Drawing.Point(11, 13);
            this.ButtonInstall.Name = "ButtonInstall";
            this.ButtonInstall.Size = new System.Drawing.Size(85, 38);
            this.ButtonInstall.TabIndex = 0;
            this.ButtonInstall.Text = "&Install";
            this.ButtonInstall.UseVisualStyleBackColor = true;
            this.ButtonInstall.Click += new System.EventHandler(this.ButtonInstall_Click);
            // 
            // ButtonUninstall
            // 
            this.ButtonUninstall.Location = new System.Drawing.Point(110, 13);
            this.ButtonUninstall.Name = "ButtonUninstall";
            this.ButtonUninstall.Size = new System.Drawing.Size(85, 38);
            this.ButtonUninstall.TabIndex = 1;
            this.ButtonUninstall.Text = "&Uninstall";
            this.ButtonUninstall.UseVisualStyleBackColor = true;
            this.ButtonUninstall.Click += new System.EventHandler(this.ButtonUninstall_Click);
            // 
            // ButtonCopyFile
            // 
            this.ButtonCopyFile.Location = new System.Drawing.Point(209, 13);
            this.ButtonCopyFile.Name = "ButtonCopyFile";
            this.ButtonCopyFile.Size = new System.Drawing.Size(85, 38);
            this.ButtonCopyFile.TabIndex = 2;
            this.ButtonCopyFile.Text = "&Copy File";
            this.ButtonCopyFile.UseVisualStyleBackColor = true;
            this.ButtonCopyFile.Click += new System.EventHandler(this.ButtonCopyFile_Click);
            // 
            // ButtonGetInfo
            // 
            this.ButtonGetInfo.Location = new System.Drawing.Point(308, 13);
            this.ButtonGetInfo.Name = "ButtonGetInfo";
            this.ButtonGetInfo.Size = new System.Drawing.Size(85, 38);
            this.ButtonGetInfo.TabIndex = 3;
            this.ButtonGetInfo.Text = "&Get Info";
            this.ButtonGetInfo.UseVisualStyleBackColor = true;
            this.ButtonGetInfo.Click += new System.EventHandler(this.ButtonGetInfo_Click);
            // 
            // ButtonAbout
            // 
            this.ButtonAbout.Location = new System.Drawing.Point(814, 13);
            this.ButtonAbout.Name = "ButtonAbout";
            this.ButtonAbout.Size = new System.Drawing.Size(85, 38);
            this.ButtonAbout.TabIndex = 8;
            this.ButtonAbout.Text = "&About";
            this.ButtonAbout.UseVisualStyleBackColor = true;
            this.ButtonAbout.Click += new System.EventHandler(this.ButtonAbout_Click);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(1012, 13);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(85, 38);
            this.ButtonExit.TabIndex = 10;
            this.ButtonExit.Text = "&Exit";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // ButtonHelp
            // 
            this.ButtonHelp.Location = new System.Drawing.Point(913, 13);
            this.ButtonHelp.Name = "ButtonHelp";
            this.ButtonHelp.Size = new System.Drawing.Size(85, 38);
            this.ButtonHelp.TabIndex = 9;
            this.ButtonHelp.Text = "&Help";
            this.ButtonHelp.UseVisualStyleBackColor = true;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Location = new System.Drawing.Point(517, 13);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(85, 38);
            this.ButtonRefresh.TabIndex = 5;
            this.ButtonRefresh.Text = "&Refresh";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point(616, 13);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(85, 38);
            this.ButtonClear.TabIndex = 6;
            this.ButtonClear.Text = "&Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // ButtonSettings
            // 
            this.ButtonSettings.Location = new System.Drawing.Point(715, 13);
            this.ButtonSettings.Name = "ButtonSettings";
            this.ButtonSettings.Size = new System.Drawing.Size(85, 38);
            this.ButtonSettings.TabIndex = 7;
            this.ButtonSettings.Text = "Settings";
            this.ButtonSettings.UseVisualStyleBackColor = true;
            this.ButtonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButtonGetInfoOutput);
            this.groupBox1.Controls.Add(this.ButtonSettings);
            this.groupBox1.Controls.Add(this.ButtonClear);
            this.groupBox1.Controls.Add(this.ButtonRefresh);
            this.groupBox1.Controls.Add(this.ButtonHelp);
            this.groupBox1.Controls.Add(this.ButtonExit);
            this.groupBox1.Controls.Add(this.ButtonAbout);
            this.groupBox1.Controls.Add(this.ButtonGetInfo);
            this.groupBox1.Controls.Add(this.ButtonCopyFile);
            this.groupBox1.Controls.Add(this.ButtonUninstall);
            this.groupBox1.Controls.Add(this.ButtonInstall);
            this.groupBox1.Location = new System.Drawing.Point(9, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1118, 57);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // ButtonGetInfoOutput
            // 
            this.ButtonGetInfoOutput.Location = new System.Drawing.Point(407, 13);
            this.ButtonGetInfoOutput.Name = "ButtonGetInfoOutput";
            this.ButtonGetInfoOutput.Size = new System.Drawing.Size(96, 38);
            this.ButtonGetInfoOutput.TabIndex = 4;
            this.ButtonGetInfoOutput.Text = "Get Info Output";
            this.ButtonGetInfoOutput.UseVisualStyleBackColor = true;
            this.ButtonGetInfoOutput.Click += new System.EventHandler(this.ButtonGetInfoOutput_Click);
            // 
            // RSMCUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 645);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RSMCUI";
            this.Text = "Remote Server Management & Configuration(RSMC)";
            this.Load += new System.EventHandler(this.RSMCUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ClientGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ClientGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.Button ButtonInstall;
        private System.Windows.Forms.Button ButtonUninstall;
        private System.Windows.Forms.Button ButtonCopyFile;
        private System.Windows.Forms.Button ButtonGetInfo;
        private System.Windows.Forms.Button ButtonAbout;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Button ButtonHelp;
        private System.Windows.Forms.Button ButtonRefresh;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.Button ButtonSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hostname;
        private System.Windows.Forms.DataGridViewTextBoxColumn os;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn AntiVirusStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirewallStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobStatusMsg;
        private System.Windows.Forms.Button ButtonGetInfoOutput;

    }
}

