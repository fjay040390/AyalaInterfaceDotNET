using System.Data;
using System.Data.OleDb;
using System.IO;
using System;

namespace Ayala_Interface_dotNet.ClassCon
{
    public class classWriteFile : classReprocess
    {
        #region "Fields"
        //private OleDbDataAdapter oleFile;

        #endregion

        public classWriteFile()
        {
            TemplateConnection();
        }

        public void CopyTemplate()
        {
            //File.Copy(Environment.CurrentDirectory + "\Template\DAY.DBF","C:\Ayala\2015\NewDay.DBF");
        }
        public void GenerateFile(string dbDateStart)
        {
            DBFQuery("INSERT INTO Daily (Trandate, Oldgt, Newgt, Dlysale,TotDisc, Totref, Totcan, Vat, TentName, Beginv, Endinv, Begor, Endor, Trancnt, Localtx, Servcharge, Notaxsale, Rawgross, Dlyloctax, Others, Termnum)  values "+
                    " ('" + dbDateStart + "', " + dbOldGT + "," + dbNewGT + "," + dbTotalDlySales + "," + dbTotalDiscount + ", " + dbTotalCAN + ", " + dbTotalCAN + ", " + dbTotalVat + ", " + tenantName + ", " + bill_startINV + ", " +
                    "  " + bill_endINV + "," + bill_start + "," + bill_end + "," + dbTotalTransaction + ", " + dbLocalTax + "," + dbServiceCharge + "," + dbNoTaxSales + "," + dbTotalRawGross + ", " + dbLocalTax + "," + dbTotalOthers  + ", "+ TerminalNumber + ")");           
      
            //DBFQuery("INSERT INTO Daily (Trandate) values('" + dbDateStart + "')");
        }
             
    }

}