using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Ayala_Interface_dotNet.ClassCon
{
    
     public class classDBConnection
      {

         public OleDbConnection con;
         public OleDbCommand cmd;
         public OleDbDataReader rdr;
         
         public string mdbPath;

         //database connection string
         public classDBConnection()
         {
             mdbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|dbLogin.accdb";
         }
         
         //execute query
         public void Queries(string sql)
         {
             //Open Connection
             openConnection();
             //Fire Query
             cmd = new OleDbCommand(sql,con);
             rdr = cmd.ExecuteReader();
             //rdr.Read();
            
         }
         
         //Open MDBConnection
         public void openConnection ()
         {
             con = new OleDbConnection(mdbPath);
             if (con.State == ConnectionState.Closed) 
             {
                 con.Open();
             }
         }
         //close MBDConnection
         public void closeConnection() 
         {
             con = new OleDbConnection(mdbPath);
             if (con.State == ConnectionState.Open)
             {
                 con.Close();
             }
         }

         
      }
}
