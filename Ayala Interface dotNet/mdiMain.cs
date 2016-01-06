using System;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet
{
    public partial class mdiMain : Form
    {
        private int childFormNumber = 0;

        public mdiMain()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolConfiguration_Click(object sender, EventArgs e)
        {
            Form frmConfig = new frmConfig();
            frmConfig.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form frmReprocess = new frmReprocess();
            frmReprocess.ShowDialog();
        }
     }
}
