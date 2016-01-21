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
         public OleDbConnection rmCon;
         public OleDbConnection tempCon;
         public OleDbCommand cmd;
         public OleDbDataReader rdr;
         public OleDbDataAdapter daTax;
         public OleDbDataAdapter daDiscount;
         public OleDbDataAdapter daLessVAT;
         public OleDbCommandBuilder cmdb;
         
         public string rmPath { get; set; }
         public string mdbPath;
         public string tempPath;
        
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
             rmPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rmPath + ";Extended Properties=dBASE IV;User ID=Admin;Password=;";
             rmCon = new OleDbConnection(rmPath);
             if (rmCon.State == ConnectionState.Closed)
             {
                 rmCon.Open();
             }
         }

         public void TemplateConnection()
         {
             tempPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Template;Extended Properties=dBASE IV;User ID=Admin;Password=;";
             tempCon = new OleDbConnection(tempPath);
             if (tempCon.State == ConnectionState.Closed)
             {
                 tempCon.Open();
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
         
         //execute computation to dbf query
         public void DBFQuery (string sql)
         {
             cmd = new OleDbCommand(sql, tempCon);
             cmd.ExecuteNonQuery();
         }

         #endregion
              
     }

}
