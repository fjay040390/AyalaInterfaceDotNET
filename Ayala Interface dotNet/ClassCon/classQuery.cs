using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
namespace Ayala_Interface_dotNet.ClassCon
{
    public class classQuery : classDBConnection  
    {

        #region "Properties"

        public string userPass { get; set; }
        public string adminPass { get; set; }
        public string printerPath { get; set; }
        public string pathRM { get; set; }
        public string tenantName { get; set; }
        public string tenantCode { get; set; }
        public string tenantContract { get; set; }
        public string ayalaFolder { get; set; }
        public string currentYear { get; set; }
        public string ayalaFolderPath { get; set; }

        public DataTable dtTax { get; set; }
        public DataTable dtDiscount { get; set; }
        public DataTable dtLessVAT { get; set; }

        public bool FolderExist { get; set; }

        #endregion

        #region "Queries Command"
        
        //get loginDetailsForLoginForm
        public void GetLoginDetails()
        {
            //SELECT query for login//fire query
            Queries("Select userPassword,adminPassword FROM tblLogin");
            //rdr.Read();
            if (rdr.HasRows) {
                //read data from database
                userPass = rdr.GetValue(0).ToString();
                adminPass = rdr.GetValue(1).ToString();                    
            }
        }

        //loadData to Config RMpath,PrinterPath,user and admin pass
        public void LoadConfigDetails()
        {
            Queries("Select * FROM tblLogin");
            if (rdr.HasRows)
            {
                userPass = rdr.GetValue(0).ToString();
                adminPass = rdr.GetValue(1).ToString();
                rmPath = rdr.GetValue(2).ToString();
                printerPath = rdr.GetValue(3).ToString();
                tenantCode = rdr.GetValue(4).ToString();
                tenantName = rdr.GetValue(5).ToString();
                tenantContract = rdr.GetValue(6).ToString();
                ayalaFolder = rdr.GetValue(7).ToString();
            }
        }
       
        //loadtaxmap
        public void LoadTaxMap() 
        {
            LoadDataGridViewTax("SELECT TaxTitle as Description, TaxMap as ID,TaxPLU as PLU FROM tblTaxMapping");
            dtTax = new DataTable();
            daTax.Fill(dtTax);
        }
    
        //loadRMDiscount
        public void LoadDiscount() 
        {
            LoadDataGridViewDiscount("SELECT DscMap as ID, DscPLU as PLU FROM tblRMDiscount");
            dtDiscount = new DataTable();
            daDiscount.Fill(dtDiscount);
        }

        //loadRMLessVAT
        public void LoadLessVAT()
        {
            LoadDataGridViewLessVAT("SELECT DscMap as ID , DscVAT as PLU FROM tblRMLessVAT");
            dtLessVAT = new DataTable();
            daLessVAT.Fill(dtLessVAT);
        }
        #endregion

        #region "Save queries"
        //save to mdb
        public void UpdateConfig(string dbParameter, OleDbType dbFieldType, int dbSize, string dbField, string dbValue)
        {
            //Open Connection
            openConnection();
            //Fire Query
            cmd = new OleDbCommand("UPDATE tblLogin SET " + dbField + " = " + dbParameter, con);
            cmd.Parameters.Add(dbParameter, dbFieldType, dbSize, dbField).Value = dbValue;
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region "check if follder exist"

        public void CheckFolderAyala()
        {
            currentYear = DateTime.Today.ToString("yyyy");
            if (!Directory.Exists(ayalaFolder + currentYear))
            {
                Directory.CreateDirectory(ayalaFolder + currentYear);
            }
            ayalaFolderPath = ayalaFolder + currentYear;
        }
        #endregion

    }
}
