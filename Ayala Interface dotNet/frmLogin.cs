using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Data.OleDb;
using Ayala_Interface_dotNet.ClassCon;


namespace Ayala_Interface_dotNet
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }
        
    //login
        private void button1_Click(object sender, EventArgs e)
        {
            classQuery loginConnect = new classQuery();
            loginConnect.GetLoginDetails();
            if (txtPassword.Text != "") {
                if (txtPassword.Text == loginConnect.userPass) {
                    mdiMain mdiMain = new mdiMain();
                    mdiMain.WindowState = FormWindowState.Maximized;
                    mdiMain.isConfigUnlocked = false;
                    mdiMain.Show();
                    this.Hide();
                } else if (txtPassword.Text == loginConnect.adminPass) {
                    mdiMain mdiMain = new mdiMain();
                    mdiMain.WindowState = FormWindowState.Maximized;
                    mdiMain.isConfigUnlocked = true;
                    mdiMain.Show();
                    this.Hide();
                } else {
                    MessageBox.Show("Access Denied!");
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            } else {
                MessageBox.Show("Enter password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtPassword.Focus();
            }
        }
       
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
