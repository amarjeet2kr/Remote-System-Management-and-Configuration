namespace RSMCUI
{
    partial class Settings
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
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ReposPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ReposUsernameTextBox = new System.Windows.Forms.TextBox();
            this.ReposMountDriveLetterListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ReposUserNameLable = new System.Windows.Forms.Label();
            this.StatusPortTextBox = new System.Windows.Forms.TextBox();
            this.CommPortTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxClientCount = new System.Windows.Forms.NumericUpDown();
            this.MaxClientLable = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.JobPullFreq = new System.Windows.Forms.NumericUpDown();
            this.PerformMonitCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MonitFreq = new System.Windows.Forms.NumericUpDown();
            this.FirewallCheckBox = new System.Windows.Forms.CheckBox();
            this.MonitUpdateLabel = new System.Windows.Forms.Label();
            this.AntivirusCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxClientCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JobPullFreq)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonitFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(207, 302);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(114, 32);
            this.ButtonSave.TabIndex = 0;
            this.ButtonSave.Text = "&Save Settings";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(359, 302);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(114, 32);
            this.ButtonCancel.TabIndex = 1;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ReposPasswordTextBox);
            this.groupBox1.Controls.Add(this.ReposUsernameTextBox);
            this.groupBox1.Controls.Add(this.ReposMountDriveLetterListBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ReposUserNameLable);
            this.groupBox1.Controls.Add(this.StatusPortTextBox);
            this.groupBox1.Controls.Add(this.CommPortTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.MaxClientCount);
            this.groupBox1.Controls.Add(this.MaxClientLable);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 262);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Configurations";
            // 
            // ReposPasswordTextBox
            // 
            this.ReposPasswordTextBox.Location = new System.Drawing.Point(150, 179);
            this.ReposPasswordTextBox.Name = "ReposPasswordTextBox";
            this.ReposPasswordTextBox.Size = new System.Drawing.Size(139, 20);
            this.ReposPasswordTextBox.TabIndex = 11;
            this.ReposPasswordTextBox.Text = "Password";
            this.ReposPasswordTextBox.UseSystemPasswordChar = true;
            this.ReposPasswordTextBox.WordWrap = false;
            // 
            // ReposUsernameTextBox
            // 
            this.ReposUsernameTextBox.Location = new System.Drawing.Point(150, 145);
            this.ReposUsernameTextBox.Name = "ReposUsernameTextBox";
            this.ReposUsernameTextBox.Size = new System.Drawing.Size(140, 20);
            this.ReposUsernameTextBox.TabIndex = 10;
            this.ReposUsernameTextBox.Text = "Username";
            this.ReposUsernameTextBox.WordWrap = false;
            // 
            // ReposMountDriveLetterListBox
            // 
            this.ReposMountDriveLetterListBox.FormattingEnabled = true;
            this.ReposMountDriveLetterListBox.Items.AddRange(new object[] {
            "H:",
            "G:",
            "I:",
            "K:",
            "L:",
            "M:",
            "N:",
            "O:",
            "P:",
            "Q:",
            "R:",
            "S:",
            "T:",
            "U:",
            "V:",
            "W:",
            "X:",
            "Y:",
            "Z:"});
            this.ReposMountDriveLetterListBox.Location = new System.Drawing.Point(235, 212);
            this.ReposMountDriveLetterListBox.Name = "ReposMountDriveLetterListBox";
            this.ReposMountDriveLetterListBox.Size = new System.Drawing.Size(44, 43);
            this.ReposMountDriveLetterListBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Repository Mount Drive Letter on Client:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Repository Password:";
            // 
            // ReposUserNameLable
            // 
            this.ReposUserNameLable.AutoSize = true;
            this.ReposUserNameLable.Location = new System.Drawing.Point(15, 148);
            this.ReposUserNameLable.Name = "ReposUserNameLable";
            this.ReposUserNameLable.Size = new System.Drawing.Size(116, 13);
            this.ReposUserNameLable.TabIndex = 6;
            this.ReposUserNameLable.Text = "Repository User Name:";
            // 
            // StatusPortTextBox
            // 
            this.StatusPortTextBox.Location = new System.Drawing.Point(223, 113);
            this.StatusPortTextBox.Name = "StatusPortTextBox";
            this.StatusPortTextBox.Size = new System.Drawing.Size(56, 20);
            this.StatusPortTextBox.TabIndex = 5;
            this.StatusPortTextBox.Text = "55556";
            // 
            // CommPortTextBox
            // 
            this.CommPortTextBox.Location = new System.Drawing.Point(223, 77);
            this.CommPortTextBox.Name = "CommPortTextBox";
            this.CommPortTextBox.Size = new System.Drawing.Size(56, 20);
            this.CommPortTextBox.TabIndex = 4;
            this.CommPortTextBox.Text = "55555";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Job Status Update Port(1 - 65535):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server Communication Port(1 - 65535):";
            // 
            // MaxClientCount
            // 
            this.MaxClientCount.Location = new System.Drawing.Point(223, 41);
            this.MaxClientCount.Name = "MaxClientCount";
            this.MaxClientCount.Size = new System.Drawing.Size(56, 20);
            this.MaxClientCount.TabIndex = 1;
            this.MaxClientCount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // MaxClientLable
            // 
            this.MaxClientLable.AutoSize = true;
            this.MaxClientLable.Location = new System.Drawing.Point(15, 44);
            this.MaxClientLable.Name = "MaxClientLable";
            this.MaxClientLable.Size = new System.Drawing.Size(187, 13);
            this.MaxClientLable.TabIndex = 0;
            this.MaxClientLable.Text = "Maximum number of clients supported:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.JobPullFreq);
            this.groupBox2.Controls.Add(this.PerformMonitCheckBox);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(359, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 262);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Configurations";
            // 
            // JobPullFreq
            // 
            this.JobPullFreq.Location = new System.Drawing.Point(227, 42);
            this.JobPullFreq.Name = "JobPullFreq";
            this.JobPullFreq.Size = new System.Drawing.Size(53, 20);
            this.JobPullFreq.TabIndex = 4;
            this.JobPullFreq.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // PerformMonitCheckBox
            // 
            this.PerformMonitCheckBox.AutoSize = true;
            this.PerformMonitCheckBox.Location = new System.Drawing.Point(25, 74);
            this.PerformMonitCheckBox.Name = "PerformMonitCheckBox";
            this.PerformMonitCheckBox.Size = new System.Drawing.Size(114, 17);
            this.PerformMonitCheckBox.TabIndex = 3;
            this.PerformMonitCheckBox.Text = "Perform Monitoring";
            this.PerformMonitCheckBox.UseVisualStyleBackColor = true;
            this.PerformMonitCheckBox.CheckedChanged += new System.EventHandler(this.PerformMonitCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MonitFreq);
            this.groupBox3.Controls.Add(this.FirewallCheckBox);
            this.groupBox3.Controls.Add(this.MonitUpdateLabel);
            this.groupBox3.Controls.Add(this.AntivirusCheckbox);
            this.groupBox3.Location = new System.Drawing.Point(22, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 144);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Monitor && Alert";
            // 
            // MonitFreq
            // 
            this.MonitFreq.Enabled = false;
            this.MonitFreq.Location = new System.Drawing.Point(205, 32);
            this.MonitFreq.Name = "MonitFreq";
            this.MonitFreq.Size = new System.Drawing.Size(53, 20);
            this.MonitFreq.TabIndex = 2;
            this.MonitFreq.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // FirewallCheckBox
            // 
            this.FirewallCheckBox.AutoSize = true;
            this.FirewallCheckBox.Enabled = false;
            this.FirewallCheckBox.Location = new System.Drawing.Point(13, 101);
            this.FirewallCheckBox.Name = "FirewallCheckBox";
            this.FirewallCheckBox.Size = new System.Drawing.Size(135, 17);
            this.FirewallCheckBox.TabIndex = 1;
            this.FirewallCheckBox.Text = "Firewall software status";
            this.FirewallCheckBox.UseVisualStyleBackColor = true;
            // 
            // MonitUpdateLabel
            // 
            this.MonitUpdateLabel.AutoSize = true;
            this.MonitUpdateLabel.Enabled = false;
            this.MonitUpdateLabel.Location = new System.Drawing.Point(10, 34);
            this.MonitUpdateLabel.Name = "MonitUpdateLabel";
            this.MonitUpdateLabel.Size = new System.Drawing.Size(194, 13);
            this.MonitUpdateLabel.TabIndex = 1;
            this.MonitUpdateLabel.Text = "Montoring Update Frequency(seconds):";
            // 
            // AntivirusCheckbox
            // 
            this.AntivirusCheckbox.AutoSize = true;
            this.AntivirusCheckbox.Enabled = false;
            this.AntivirusCheckbox.Location = new System.Drawing.Point(12, 67);
            this.AntivirusCheckbox.Name = "AntivirusCheckbox";
            this.AntivirusCheckbox.Size = new System.Drawing.Size(140, 17);
            this.AntivirusCheckbox.TabIndex = 0;
            this.AntivirusCheckbox.Text = "Antivirus software status";
            this.AntivirusCheckbox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server Job Pull Frequency(seconds):";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 350);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxClientCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JobPullFreq)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonitFreq)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MaxClientCount;
        private System.Windows.Forms.Label MaxClientLable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox StatusPortTextBox;
        private System.Windows.Forms.TextBox CommPortTextBox;
        private System.Windows.Forms.CheckBox PerformMonitCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown MonitFreq;
        private System.Windows.Forms.CheckBox FirewallCheckBox;
        private System.Windows.Forms.Label MonitUpdateLabel;
        private System.Windows.Forms.CheckBox AntivirusCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown JobPullFreq;
        private System.Windows.Forms.TextBox ReposPasswordTextBox;
        private System.Windows.Forms.TextBox ReposUsernameTextBox;
        private System.Windows.Forms.ListBox ReposMountDriveLetterListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ReposUserNameLable;
    }
}