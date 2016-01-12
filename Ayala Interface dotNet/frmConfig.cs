using System;
using System.Windows.Forms;
using System.Data.OleDb;
using Ayala_Interface_dotNet.ClassCon;

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
            classQuery loadData = new classQuery();
           //loadData
            loadData.LoadLoginData();
            txtAdminPass.Text = loadData.adminPass;
            txtUserPass.Text = loadData.userPass;
            txtRMPath.Text = loadData.pathRM;
            txtPrinterPath.Text = loadData.printerPath;
            txtTenantCode.Text = loadData.tenantCode;
            txtTenantName.Text = loadData.tenantName;
            txtContractNumber.Text = loadData.tenantContract;

            loadData.LoadTaxMap();
            dgwTaxTable.DataSource = loadData.dt;
            loadData.dt.Dispose();

            loadData.LoadDiscount();
            dgwDiscountNonVat.DataSource = loadData.dt;
            loadData.dt.Dispose();
        }

        private void btnBrowseRM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseRM = new FolderBrowserDialog();
            browseRM.ShowDialog();
            if (browseRM.SelectedPath != null) {
                txtRMPath.Text = browseRM.SelectedPath.ToString();
            }
               
        }

        private void dgwTaxTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*classQuery saveData = new classQuery();
            saveData.SaveData();
            txtUserPass.Text = saveData.userPass;
            */

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
