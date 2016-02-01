using System.Data;
using System.Data.OleDb;


namespace Ayala_Interface_dotNet.ClassCon
{
    
     public class classDBConnection 
     {
         #region "Declaration

         public OleDbConnection con;
         public OleDbConnection rmCon;
         public OleDbConnection tempCon;
         public OleDbCommand cmd;
         public OleDbDataReader rdr;
         public OleDbDataAdapter daTax;
         public OleDbDataAdapter daDiscount;
         public OleDbDataAdapter daLessVAT;
         public OleDbDataAdapter daDiplomat;
         public OleDbDataAdapter daDeleteRecords;
         public OleDbDataAdapter daTerminal;
         public OleDbDataAdapter daGT; 
         public OleDbCommandBuilder cmdb;
         
         public string rmPath { get; set; }
         public string mdbPath;
         public string tempPath;

         public DataTable dtGrandTotal { get; set; }
         #endregion

         #region "Connection Routine"

         //database connection string
         public classDBConnection()
         {
             mdbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|dbLogin.accdb";
         }
               
         //Open MDBConnection
         public void openConnection()
         {
            con = new OleDbConnection(mdbPath);          
             if (con.State == ConnectionState.Closed) {
                 con.Open();
             }
         }

         public void rmConnect()
         {
             string rmConStr;
             rmConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rmPath + ";Extended Properties=dBASE IV;User ID=Admin;Password=;";
             rmCon = new OleDbConnection(rmConStr);
             if (rmCon.State == ConnectionState.Closed)
             {
                 rmCon.Open();
             }
         }

         public void rmDisconnect()
         {
             if (rmCon.State == ConnectionState.Open)
             {
                 rmCon.Close();
                 rmCon.Dispose();
             }
         }

         public void TemplateConnection(string filePath)
         {
             tempPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=dBASE IV;User ID=Admin;Password=;";
             tempCon = new OleDbConnection(tempPath);
             if (tempCon.State == ConnectionState.Closed)
             {
                 tempCon.Open();
             }
         }

         public void TemplateConnectionClose()
         {
             if (tempCon.State == ConnectionState.Open)
             {
                 tempCon.Close();
             }
         }

         //close MBDConnection
         public void closeConnection() 
         {
             con = new OleDbConnection(mdbPath);
             if (con.State == ConnectionState.Open) {
                 con.Close();
             }
         }

         public void closeRMConnect() 
         {
             if (rmCon.State == ConnectionState.Open) {
                 rmCon.Close();
             }
         }
 
         #endregion

         #region "Queries Routine"
        
         //execute mDB query
         public void Queries(string sql)
         {
             //Open Connection
             openConnection();
             //Fire Query
             cmd = new OleDbCommand(sql, con);
             rdr = cmd.ExecuteReader();
             rdr.Read();
         }

         public void RMQueries(string sql)
         {
             //Fire Query
             cmd = new OleDbCommand(sql, rmCon);
             rdr = cmd.ExecuteReader();
             rdr.Read();
         }
  
         //execute datagridview query
         public void LoadDataGridViewTax(string sql)
         {
             daTax = new OleDbDataAdapter();
             openConnection();
             daTax = new OleDbDataAdapter(sql, con);
         }

         public void LoadDataGridViewDiscount(string sql)
         {
             daDiscount = new OleDbDataAdapter();
             openConnection();
             daDiscount = new OleDbDataAdapter(sql, con);
         }

         public void LoadDataGridViewLessVAT(string sql)
         {
             daLessVAT = new OleDbDataAdapter();
             openConnection();
             daLessVAT = new OleDbDataAdapter(sql, con);
         }
         
         public void LoadDataGridViewDiplomat(string sql)
         {
             daDiplomat = new OleDbDataAdapter();
             openConnection();
             daDiplomat = new OleDbDataAdapter(sql, con);
         }

         public void LoadDataGridViewTerminal(string sql)
         {
             daTerminal = new OleDbDataAdapter();
             openConnection();
             daTerminal = new OleDbDataAdapter(sql, con);
         }

         public void LoadOldGrandTotal(string sql)
         {
             daGT = new OleDbDataAdapter();
             //Open Connection
             openConnection();
             //Fire Query
             daGT = new OleDbDataAdapter(sql, con);
             dtGrandTotal = new DataTable();
             daGT.Fill(dtGrandTotal);
         }

         //execute computation to dbf query
         public void DBFQuery (string sql)
         {
             cmd = new OleDbCommand(sql, tempCon);
             cmd.ExecuteNonQuery();
         }
      
         #endregion
              
     }

}
