using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace RSMCUI
{
    public partial class RSMCUI : Form
    {
        public string client_ip;
        public string software_name;
        public string software_version;
        public string file;
        public string info_action;

        public RSMCUI()
        {
            InitializeComponent();
        }
        private void Refresh_ClientTable()
        {
            string clientlist = RSMCPipe.RefreshClients(100);
            if (!clientlist.Equals("None"))
            {
                string[] clientarray = RSMCPipe.GetClientsArray(clientlist);
                int i = 0;
                foreach (string c in clientarray)
                {
                    string[] client_detail = RSMCPipe.GetClientDetail(c);
                    for (int j = 0; j < client_detail.Length; j++)
                    {
                        this.ClientGrid.Rows[i].Cells[j].Value = client_detail[j];
                    }
                    i++;
                }
            }
        }
        private void RSMCUI_Load(object sender, EventArgs e)
        {
            this.ClientGrid.Rows.Add(100);
            Refresh_ClientTable();
        }


        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to exit?", "RSMC", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            Refresh_ClientTable();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.ClientGrid.Rows[i].Cells[j].Value = "";
                }
            }
        }

        private void ButtonAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 abox = new AboutBox1();
            abox.ShowDialog();
        }

        private void ButtonInstall_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = this.ClientGrid.SelectedRows;
            if (row.Count == 0)
            {
                MessageBox.Show("Select a complete row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            client_ip = row[0].Cells[0].Value.ToString();
            Install install = new Install();
            install.ShowDialog();
            if (!install.ok)
                return;

            char[] sep = { ' ' };
            string[] s = install.software_string.Split(sep);
            software_name = s[0];
            software_version = s[1];

            // send to server ui thread through pipe
            string req_string = "install:" + client_ip + ":" + software_name + ":" + software_version;
            int retval = RSMCPipe.SendAction(req_string, 300);
            if (retval == 0)
            {
                MessageBox.Show("Failed to send message to RSMC Server for installation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Requested RSMC Server for installation!, Check the status by clickinig on Refresh Button!", "Install", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonUninstall_Click(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection row = this.ClientGrid.SelectedRows;
            if (row.Count == 0)
            {
                MessageBox.Show("Select a complete row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            client_ip = row[0].Cells[0].Value.ToString();
            Uninstall uninstall = new Uninstall();
            uninstall.ShowDialog();
            if (!uninstall.ok)
                return;

            char[] sep = { ' ' };
            string[] s = uninstall.software_string.Split(sep);
            software_name = s[0];
            software_version = s[1];

            // send to server ui thread through pipe
            string req_string = "uninstall:" + client_ip + ":" + software_name + ":" + software_version;
            int retval = RSMCPipe.SendAction(req_string, 300);
            if (retval == 0)
            {
                MessageBox.Show("Failed to send message to RSMC Server for uninstallation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Requested RSMC Server for Uninstallation!, Check the status by clickinig on Refresh Button!", "UnInstall", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonCopyFile_Click(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection row = this.ClientGrid.SelectedRows;
            if (row.Count == 0)
            {
                MessageBox.Show("Select a complete row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            client_ip = row[0].Cells[0].Value.ToString();

            file = Interaction.InputBox("What is the name of the file that you would wish to copy to Client?", "Copy File", "Default Text");
            if (file.Equals("Default Text") || String.IsNullOrWhiteSpace(file))
                return;
            // send to server ui thread through pipe
            string req_string = "copy:" + client_ip + ":" + file;
            int retval = RSMCPipe.SendAction(req_string, 300);
            if (retval == 0)
            {
                MessageBox.Show("Failed to send message to RSMC Server for Copying File!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Requested RSMC Server for Copying File!, Check the status by clickinig on Refresh Button!", "Copy File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonGetInfo_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = this.ClientGrid.SelectedRows;
            if (row.Count == 0)
            {
                MessageBox.Show("Select a complete row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            client_ip = row[0].Cells[0].Value.ToString();

            info_action = Interaction.InputBox("Get Information can provide details like Services, Installed Softwares, Disk Details, Running Tasks. Enter your choice(Service,Softwares,Disk,Process) ?", "Copy File", "Default Text");
            if (info_action.Equals("Default Text") || String.IsNullOrWhiteSpace(info_action))
                return;
            // send to server ui thread through pipe
            string req_string = "getinfo:" + client_ip + ":" + info_action;
            int retval = RSMCPipe.SendAction(req_string, 300);
            if (retval == 0)
            {
                MessageBox.Show("Failed to send message to RSMC Server for Getting Info!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Requested RSMC Server for Getting Info!, Check the status by clickinig on Refresh Button!", "GetInfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void ClientGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ButtonGetInfoOutput_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = this.ClientGrid.SelectedRows;
            if (row.Count == 0)
            {
                MessageBox.Show("Select a complete row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            client_ip = row[0].Cells[0].Value.ToString();

            string fname = "c:\\repos\\getinfo_" + client_ip + "_.out";
            Globals.RunCommand("notepad.exe", fname, false);
        }
    }
}
