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
            if (txtPassword.Text == loginConnect.userPass)
            {
                Form mdiMain = new mdiMain();
                mdiMain.WindowState = FormWindowState.Maximized;
                mdiMain.Show();
                this.Hide();
            }
            else if (txtPassword.Text == loginConnect.adminPass)
            {
                Form mdiMain = new mdiMain();
                mdiMain.WindowState = FormWindowState.Maximized;
                mdiMain.Show();
                
                this.Hide();
            } else {
                MessageBox.Show("Please enter you password!");
                txtPassword.Text = "";
            }
   
        }
       
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
