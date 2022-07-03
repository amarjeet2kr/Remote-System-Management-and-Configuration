using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSMCUI
{
    public partial class Settings : Form
    {
        // Server configs
        public int CommunicationPort;
        public int JobUpdatePort;
        public int MaxClients;

        //Client Configs
        public int PullFrequency;
        public bool PerformMonitoring;
        public int MonitoringFrequeny;
        public bool MonitorDefenderAntivirus;
        public bool MonitorDefenderFirewall;

        public string Repos_UserName;
        public string Repos_Password;
        public string Repos_MountDriveLetter;

        public Settings()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            CommunicationPort = Int32.Parse(CommPortTextBox.Text.ToString());
            JobUpdatePort = Int32.Parse(StatusPortTextBox.Text.ToString());
            MaxClients = (int)MaxClientCount.Value;

            PullFrequency = (int)JobPullFreq.Value;
            PerformMonitoring = PerformMonitCheckBox.Checked;
            MonitoringFrequeny = Int32.Parse(MonitFreq.Text.ToString());
            MonitorDefenderAntivirus = AntivirusCheckbox.Checked;
            MonitorDefenderFirewall = FirewallCheckBox.Checked;
            this.Close();
        }

        private void PerformMonitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PerformMonitCheckBox.Checked)
            {
                MonitFreq.Enabled = true;
                AntivirusCheckbox.Enabled = true;
                FirewallCheckBox.Enabled = true;
                MonitUpdateLabel.Enabled = true;
            }
            else
            {
                MonitFreq.Enabled = false;
                AntivirusCheckbox.Enabled = false;
                FirewallCheckBox.Enabled = false;
                MonitUpdateLabel.Enabled = false;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     }
}
