using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;


namespace Ayala_Interface_dotNet.ClassCon
{
    
     public class classDBConnection 
     {
         #region "Declaration

         public OleDbConnection con;
         public OleDbCommand cmd;
         public OleDbDataReader rdr;
         public OleDbDataAdapter daTax;
         public OleDbDataAdapter daDiscount;
         public OleDbCommandBuilder cmdb;
         public string rmPath { get; set; }
         public string RMPATH;
         public string mdbPath;
        
         
        
        #endregion

         #region "Connection Routine"

         //database connection string
         public classDBConnection()
         {
             mdbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|dbLogin.accdb";
             RMPATH = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rmPath + ";Extended Properties=dBASE IV;User ID=Admin;Password=;";
         }
               
         //Open MDBConnection
         public void openConnection()
         {
            con = new OleDbConnection(mdbPath);          
             if (con.State == ConnectionState.Closed) {
                 con.Open();
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

         //Open RMPathConnection
         public  void openRMConnection() 
         {
             con = new OleDbConnection(RMPATH);
            if (con.State == ConnectionState.Closed) {
                con.Open();
            }
         }   

         //Close RMPathConnection
         public void closeRMPathConnection()
         {
             con = new OleDbConnection(RMPATH);
             if (con.State == ConnectionState.Open) {
                 con.Close();
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
         }

         public void RMQueries(string sql)
         {
             //Open Connection
             openRMConnection();
             //Fire Query
             cmd = new OleDbCommand(sql, con);
             rdr = cmd.ExecuteReader();
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
        #endregion
              
     }

}
