﻿using System;
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
         public OleDbDataAdapter da;
         public string mdbPath;
         public string rmPath;

         //database connection string
         public classDBConnection()
         {
             mdbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|dbLogin.accdb";
             rmPath = "Provider=Microsoft.Jet.OleDB.4.0;Data Source= " + rmPath + "; Extended Properties=dBase IV;User ID=Admin;Password=;";
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
             if (con.State == ConnectionState.Open) {
                 con.Close();
             }
         }

         //Open RMPathConnection
         public void openRMPathConnection() 
         {
             con = new OleDbConnection(rmPath);
             if (con.State == ConnectionState.Closed) {
                 con.Open();
             }
         }

         //Close RMPathConnection
         public void closeRMPathConnection()
         {
             con = new OleDbConnection(rmPath);
             if (con.State == ConnectionState.Open) {
                 con.Close();
             }
         }

         //execute query
         public void Queries(string sql)
         {
             //Open Connection
             openConnection();
             //Fire Query
             cmd = new OleDbCommand(sql, con);
             rdr = cmd.ExecuteReader();
         }

         //execute datagridview query
         public void datagridQuery(string sql)
         {
             openConnection();
             da = new OleDbDataAdapter(sql, con);
         }
         
      }

}
