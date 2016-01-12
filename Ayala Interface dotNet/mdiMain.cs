using System;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet
{
    public partial class mdiMain : Form
    {
        
        public mdiMain()
        {
            InitializeComponent();
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

        private void mdiMain_Load(object sender, EventArgs e)
        {

        }

        #region "Properties"
        
        public Boolean isConfigUnlocked {
            get { return this.toolConfiguration.Enabled; }
            set { this.toolConfiguration.Enabled = value; } 
        }

        #endregion
    }
}
