using System;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form mdiMain = new mdiMain();
            mdiMain.WindowState = FormWindowState.Maximized;
            mdiMain.Show();
            this.Hide();
        }
    }
}
