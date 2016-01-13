using System.Data;
using System.Data.OleDb;
namespace Ayala_Interface_dotNet.ClassCon
{
    public class classQuery : classDBConnection
    {
        #region "Properties"
        public string userPass { get; set; }
        public string adminPass { get; set; }
        public string pathRM { get; set; }
        public string printerPath { get; set; }
        public string tenantName { get; set; }
        public string tenantCode { get; set; }
        public string tenantContract { get; set; }
                
        public DataTable dtTax { get; set; }
        public DataTable dtDiscount { get; set; }
        #endregion
        public classQuery()
        {
            
        }
        #region "Queries Command"
        //get loginDetailsForLoginForm
        public void GetLoginDetails()
        {
            //SELECT query for login//fire query
            Queries("Select userPassword,adminPassword FROM tblLogin");
            //rdr.Read();
            if (rdr.HasRows) {
                //read data from database
                while (rdr.Read()) {
                    userPass = rdr.GetValue(0).ToString();
                    adminPass = rdr.GetValue(1).ToString();                    
                }
            }
        }

        //loadData for Config RMpath,PrinterPath,user and admin pass
        public void LoadLoginData()
        {
            Queries("Select * FROM tblLogin");
            if (rdr.HasRows) {
                while (rdr.Read()) {
                    userPass = rdr.GetValue(0).ToString();
                    adminPass = rdr.GetValue(1).ToString();
                    pathRM = rdr.GetValue(2).ToString();
                    printerPath = rdr.GetValue(3).ToString();
                    tenantCode = rdr.GetValue(4).ToString();
                    tenantName = rdr.GetValue(5).ToString();
                    tenantContract = rdr.GetValue(6).ToString();
                }
            }
        }
       
        //loadtaxmap
        public void LoadTaxMap() {
            LoadDataGridViewTax("SELECT TaxTitle,TaxMap,TaxPLU FROM tblTaxMapping");
            dtTax = new DataTable();
            daTax.Fill(dtTax);
        }
    
        //loadRMDiscount
        public void LoadDiscount() {
            LoadDataGridViewDiscount("SELECT DscMap, DscPLU FROM tblRMDiscount");
            dtDiscount = new DataTable();
            daDiscount.Fill(dtDiscount);
        }

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

    }
}
