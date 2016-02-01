using System;
using System.Windows.Forms;
using Ayala_Interface_dotNet.ClassCon;
using System.Diagnostics;
using System.IO;
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

        private void toolSession_Click(object sender, EventArgs e)
        {
            classQuery classQuery = new classQuery();
            classDBConnection classDB = new classDBConnection();
            
            //Load Config
            classQuery.LoadConfigDetails();
            //Change directory to RM
            Directory.SetCurrentDirectory(classQuery.rmPath);
            //Execute sessopen.exe
            var process = Process.Start(classQuery.rmPath + "\\sessopen.exe");
            process.WaitForExit();
            //get last current session
            classDB.rmPath = classQuery.rmPath;
            classDB.rmConnect();
            classDB.RMQueries("SELECT TOP 1 session_no, date_start, date_end FROM rep" + DateTime.Today.ToString("yy") + " ORDER BY session_no DESC");
            //MessageBox.Show(classDB.rdr["date_end"].ToString());
            if (!DBNull.Value.Equals(classDB.rdr["date_end"]))
            {
                frmReprocess frmReprocess = new frmReprocess();
                frmReprocess.Show();
            }
            classDB.rmDisconnect();
        }
       
    }
}
