using System;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseRM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseRM = new FolderBrowserDialog();
            browseRM.ShowDialog();
            if (browseRM.SelectedPath != null) {
                txtRMPath.Text = browseRM.SelectedPath.ToString();
            }
               
        }

               
    }
}
