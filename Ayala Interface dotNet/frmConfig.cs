using System;
using System.Windows.Forms;
using System.Data.OleDb;
using Ayala_Interface_dotNet.ClassCon;

namespace Ayala_Interface_dotNet
{
    public partial class frmConfig : Form
    {
        classQuery classQuery = new classQuery();

        public frmConfig()
        {
            InitializeComponent();
        }

        #region "Load Data to Config"
        private void frmConfig_Load(object sender, EventArgs e)
        {
           //loadData
            classQuery.LoadLoginData();
            txtAdminPass.Text = classQuery.adminPass;
            txtUserPass.Text = classQuery.userPass;
            txtRMPath.Text = classQuery.pathRM;
            txtPrinterPath.Text = classQuery.printerPath;
            txtTenantCode.Text = classQuery.tenantCode;
            txtTenantName.Text = classQuery.tenantName;
            txtContractNumber.Text = classQuery.tenantContract;

            classQuery.LoadTaxMap();
            dgwTaxTable.DataSource = classQuery.dtTax;
            classQuery.dtTax.Dispose();

            classQuery.LoadDiscount();
            dgwDiscountNonVat.DataSource = classQuery.dtDiscount;
            classQuery.dtTax.Dispose();
        }

        #endregion

        #region "Saving data to database"

        private void btnSave_Click(object sender, EventArgs e)
        {
            classQuery.UpdateConfig("@userPass", OleDbType.Integer, 4, "userPassword", txtUserPass.Text);
            classQuery.UpdateConfig("@adminPass", OleDbType.Integer, 4, "adminPassword", txtAdminPass.Text);
            classQuery.UpdateConfig("@pathRM", OleDbType.Char, 100, "RMPath", txtRMPath.Text);
            classQuery.UpdateConfig("@printerPath", OleDbType.Char, 100, "printerPath", txtPrinterPath.Text);
            classQuery.UpdateConfig("@tenantCode", OleDbType.Char, 3, "tenantCode", txtTenantCode.Text);
            classQuery.UpdateConfig("@tenantName", OleDbType.Char, 10, "tenantName", txtTenantName.Text);
            classQuery.UpdateConfig("@tenantContract", OleDbType.Char, 100, "tenantContract", txtContractNumber.Text);
            
            //save DataGridViewTax
            OleDbCommandBuilder cmdBuild;
            cmdBuild = new OleDbCommandBuilder(classQuery.daTax);
            classQuery.daTax.Update(classQuery.dtTax);
            //save DataGridViewDiscount
            cmdBuild = new OleDbCommandBuilder(classQuery.daDiscount);
            classQuery.daDiscount.Update(classQuery.dtDiscount);
            MessageBox.Show("Update successful!");
            this.Dispose();
            MessageBox.Show(classQuery.rmPath);
        }

#endregion

        #region "BrowserFolderDialog"
        private void btnBrowseRM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseRM = new FolderBrowserDialog();
            browseRM.ShowDialog();
            if (browseRM.SelectedPath != null)
            {
                txtRMPath.Text = browseRM.SelectedPath.ToString();
            }
        }

        private void btnBrowsePrinter_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseRM = new FolderBrowserDialog();
            browseRM.ShowDialog();
            if (browseRM.SelectedPath != null) {
                txtPrinterPath.Text = browseRM.SelectedPath.ToString();
            }
        }
        #endregion  

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

      

    }
}
