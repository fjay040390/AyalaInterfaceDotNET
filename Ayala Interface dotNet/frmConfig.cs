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
            classQuery.LoadConfigDetails();
            txtAdminPass.Text = classQuery.adminPass;
            txtUserPass.Text = classQuery.userPass;
            txtRMPath.Text = classQuery.rmPath;
            txtPrinterPath.Text = classQuery.printerPath;
            txtTenantCode.Text = classQuery.tenantCode;
            txtTenantName.Text = classQuery.tenantName;
            txtContractNumber.Text = classQuery.tenantContract;
            txtAyalaFolder.Text = classQuery.ayalaFolder;

            classQuery.LoadTaxMap();
            dgwTaxTable.DataSource = classQuery.dtTax;
            dgwTaxTable.Columns[0].Width = 80;
            dgwTaxTable.Columns[1].Width = 70;
            dgwTaxTable.Columns[2].Width = 70;
            classQuery.dtTax.Dispose();

            classQuery.LoadDiscount();
            dgwDiscountNonVat.DataSource = classQuery.dtDiscount;
            dgwDiscountNonVat.Columns[0].Width = 70;
            dgwDiscountNonVat.Columns[1].Width = 70;
            classQuery.dtTax.Dispose();

            classQuery.LoadLessVAT();
            dgwLessVAT.DataSource = classQuery.dtLessVAT;
            dgwLessVAT.Columns[0].Width = 70;
            dgwLessVAT.Columns[1].Width = 70;
            classQuery.dtLessVAT.Dispose();
        }

        #endregion

        #region "Saving data to database"

        private void btnSave_Click(object sender, EventArgs e)
        {
            classQuery.UpdateConfig("@userPass", OleDbType.Integer, 4, "userPassword", txtUserPass.Text);
            classQuery.UpdateConfig("@adminPass", OleDbType.Integer, 4, "adminPassword", txtAdminPass.Text);
            classQuery.UpdateConfig("@rmPath", OleDbType.Char, 100, "RMPath", txtRMPath.Text);
            classQuery.UpdateConfig("@printerPath", OleDbType.Char, 100, "printerPath", txtPrinterPath.Text);
            classQuery.UpdateConfig("@tenantCode", OleDbType.Char, 3, "tenantCode", txtTenantCode.Text);
            classQuery.UpdateConfig("@tenantName", OleDbType.Char, 10, "tenantName", txtTenantName.Text);
            classQuery.UpdateConfig("@tenantContract", OleDbType.Char, 100, "tenantContract", txtContractNumber.Text);
            classQuery.UpdateConfig("@ayalaFolder", OleDbType.Char, 100, "ayalaFolder", txtAyalaFolder.Text);
            
            //save DataGridViewTax
            OleDbCommandBuilder cmdBuild;
            cmdBuild = new OleDbCommandBuilder(classQuery.daTax);
            classQuery.daTax.Update(classQuery.dtTax);
            //save DataGridViewDiscount
            cmdBuild = new OleDbCommandBuilder(classQuery.daDiscount);
            classQuery.daDiscount.Update(classQuery.dtDiscount);
            //save DataGridViewLessVAT
            cmdBuild = new OleDbCommandBuilder(classQuery.daLessVAT);
            classQuery.daLessVAT.Update(classQuery.dtLessVAT);
            MessageBox.Show("Update successful!");
            this.Dispose();
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

        private void browseDestination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseAyala = new FolderBrowserDialog();
            browseAyala.ShowDialog();
            if (browseAyala.SelectedPath != null)
            {
                txtAyalaFolder.Text = browseAyala.SelectedPath.ToString();
            }
        }
        #endregion      

        

    }
}
