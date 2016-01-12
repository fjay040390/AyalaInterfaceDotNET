using System.Data;
using System.Data.OleDb;
namespace Ayala_Interface_dotNet.ClassCon
{
    public class classQuery : classDBConnection
    {
        public string userPass { get; set; }
        public string adminPass { get; set; }
        public string pathRM { get; set; }
        public string printerPath { get; set; }
        public string tenantName { get; set; }
        public string tenantCode { get; set; }
        public string tenantContract { get; set; }
                
        public DataTable dt { get; set; }

        public classQuery()
        {
            
        }
        
        //get loginDetailsForLoginForm
        public void GetLoginDetails()
        {
            //SELECT query for login//fire query
            Queries("Select userPassword,adminPassword FROM tblLogin");
            //rdr.Read();
            if (rdr.HasRows)
            {
                //read data from database
                while (rdr.Read())
                {
                    userPass = rdr.GetValue(0).ToString();
                    adminPass = rdr.GetValue(1).ToString();                    
                }

            }
        }

        //loadData for Config RMpath,PrinterPath,user and admin pass
        public void LoadLoginData()
        {
            Queries("Select userPassword,adminPassword,rmPath,printerPath,tenantCode,tenantName,tenantContract FROM tblLogin");
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
            datagridQuery("SELECT * FROM tblTaxMapping");
            dt = new DataTable();
            da.Fill(dt);
        }
        //loadRMDiscount
        public void LoadDiscount()
        {
            datagridQuery("SELECT * FROM tblRMDiscount");
            dt = new DataTable();
            da.Fill(dt);
        }

        //SaveData
       /* public void SaveData()
        {
            openConnection();
            Queries("UPDATE tblLogin SET userPassword = '" + userPass + "' ");
            cmd.Parameters.Add("@userPassword", OleDbType.Integer, 6).Value = userPass;
         
        }*/


    }
}
