namespace RSMCUI
{
    partial class Install
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
            this.label1 = new System.Windows.Forms.Label();
            this.softlistbox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of softwares:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // softlistbox
            // 
            this.softlistbox.FormattingEnabled = true;
            this.softlistbox.Items.AddRange(new object[] {
            "NotepadPlusPlus 7.8.4",
            "Putty 0.72",
            "CCleaner 5.63",
            "CodeBlocks 17.12",
            "TeamViwer 15.13",
            "VideoLAN(VLC) 3.0.8"});
            this.softlistbox.Location = new System.Drawing.Point(34, 39);
            this.softlistbox.Name = "softlistbox";
            this.softlistbox.Size = new System.Drawing.Size(241, 199);
            this.softlistbox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(315, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Install";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(315, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 39);
            this.button2.TabIndex = 4;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(315, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 39);
            this.button3.TabIndex = 5;
            this.button3.Text = "&Help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Location = new System.Drawing.Point(315, 191);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(73, 39);
            this.ButtonRefresh.TabIndex = 6;
            this.ButtonRefresh.Text = "&Refresh";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // Install
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 255);
            this.Controls.Add(this.ButtonRefresh);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.softlistbox);
            this.Controls.Add(this.label1);
            this.Name = "Install";
            this.Text = "Install";
            this.Load += new System.EventHandler(this.Install_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox softlistbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button ButtonRefresh;
    }
}