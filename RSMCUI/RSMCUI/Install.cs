using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RSMCUI
{
    public partial class Install : Form
    {
        public string software_string;
        public bool ok = false;
        public Install()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ok = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select any software from the list and click Install button.", "Install Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
            int index = this.softlistbox.SelectedIndex;
            software_string = this.softlistbox.Items[softlistbox.SelectedIndex].ToString();
            this.Close();
        }

        public void LoadSwList(bool fresh = false)
        {
            List<string> swl;

            if (Globals.ServerIP.Equals("localhost") || Globals.ServerIP.Equals("127.0.0.1"))
            {
                swl = Globals.GetSoftwareList(@"c:\repos\swlist.conf");
            }
            else
            {
                if (fresh)
                {
                    Globals.GetSoftwareListConfFresh();
                }
                else
                {
                    Globals.GetSoftwareListConf();
                }
                swl = Globals.GetSoftwareList("swlist.conf");
            }


            this.softlistbox.Items.Clear(); ;

            int i = 0;
            foreach (string s in swl)
            {
                this.softlistbox.Items.Insert(i, s);
                i++;
            }

        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadSwList(true);        
        }

        private void Install_Load(object sender, EventArgs e)
        {
            LoadSwList();
        }

    }
}
