using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Ayala_Interface_dotNet.ClassCon
{
    public class classReprocess : classQuery
    {
        #region "Declaration"

        public string repYear { get; set; }
        public string repMonth { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string sessNo { get; set; }
        public string bill_start { get; set; }
        public string bill_end { get; set; }

        public DateTime strDate;
        public DateTime endDate;

        #endregion

        #region "Properties"

        public string dbServiceCharge { get; set; }
        public string dbNoTaxSales { get; set; }
        public string dbLocalTax { get; set; }
        public string dbTotalOthers { get; set; }
        public string dbTotalSales { get; set; }
        public string dbTotalVat { get; set; }
        public string dbTotalTransaction { get; set; }

        public int TotalRawGross { get; set; }
        public int taxSTR { get; set; }

        #endregion

        public classReprocess()
        {
            LoadConfigDetails();
            LoadDiscount();
            LoadTaxMap();
            rmConnect();
            TemplateConnection();
            //openConnection();
        }

        #region "Save Filter"

        //FilteringDate
        public void FilterDate()
        {
            bill_start = "";
            bill_end = "";
            endDate = Convert.ToDateTime(dateEnd);
            strDate = Convert.ToDateTime(dateStart);
            do {
                repYear = strDate.ToString("yy");
                repMonth = strDate.ToString("MM");
                GetSessionNo();
                SaveFilterDate(sessNo, bill_start, bill_end);
               // GetServiceCharge(1);
               // SaveComputeToDBF(dbServiceCharge);
                strDate = strDate.AddDays(1);
            } while (strDate <= endDate);
        }
        #endregion

        #region "Utility Functions"
        //Select QUERY from REP
        public void GetSessionNo()
        {
           RMQueries("SELECT session_no,first_bill,last_bill FROM REP" + repYear + ".DBF WHERE date_start = #" + strDate + "#");
           //while (rdr.Read()) {
                sessNo = rdr.GetValue(0).ToString();
                bill_start = rdr.GetValue(1).ToString();
                bill_end = rdr.GetValue(2).ToString();
            //}
        }

        //Save data from query REP
        public void SaveFilterDate(string str_date, string bill_start, string bill_end)
        {
            Queries("INSERT INTO tblFilterDate VALUES ('" + str_date + "','" + bill_start + "','" + bill_end + "','" + repMonth + "','" + repYear + "')");
        }

        #endregion

        #region "Computation"

        //Compute Local Tax
        /*   public void ComputeLocalTax()
           {
               cmd = new OleDbCommand("SELECT Sum(Tax_Amt) As LocalTax FROM SLS" + repMonth + repYear +
               " a LEFT JOIN TAX" + repMonth + repYear + " b ON a.Bill_No = b.Bill_no WHERE a.Session_No = " +  sessNo);
           }
           */

        public void GetServiceCharge(int strTerminal)
        {
            RMQueries("SELECT sum(auto_grat) from SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo);
            dbServiceCharge = rdr.GetValue(0).ToString();
        }

        public void GetNoTaxSales(int strTerminal)
        {
            RMQueries("SELECT Sum(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND pay_type <> 5 AND Settle_stn = " + strTerminal + " AND NOT Taxable");
            dbNoTaxSales = rdr.GetValue(0).ToString();
        }

   /*    public void GetLocalTax(int strTerminal)
       { 
           RMQueries = "SELECT Sum(b.Tax_Amt) FROM SLS" + repMonth + repYear + ".DBF LEFT a JOIN TAX"+ repMonth + repYear + " b ON a.Bill_no = b.Bill_no WHERE a.Session_No = "+sessNo+" AND b.Tax_no NOT IN(" + STRNOTAXPLU + ") AND settle_stn = " + strTerminal);
           dbLocalTax = rdr.GetValue(0).ToString();
       }
    */

        public void GetTotalOthers(int strTerminal)
        {
            RMQueries("SELECT SUM(People_no) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo + " AND Pay_type <> 5 and Settle_stn = " + strTerminal);
            dbTotalOthers = rdr.GetValue(0).ToString();
        }

        public void GetTotalSales(int strTerminal)
        {
            RMQueries("SELECT SUM(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo + " AND Pay_type <> 5 and Settle_stn = " + strTerminal);
            dbTotalOthers = rdr.GetValue(0).ToString();
        }
            
        public void GetTotalVat(int strTerminal)
        {
            RMQueries("SELECT SUM(Tax_Amt) FROM SLS" + repMonth + repYear + ".DBF LEFT JOIN TAX" + repMonth + repYear + " ON Bill_No = Bill_No WHERE Pay_Type <> 5 AND  Session_no = " + sessNo + " AND Tax_No = " + taxSTR  + " OR Tax_No = " + taxSTR + " AND Settle_stn =" + strTerminal);
            dbTotalVat = rdr.GetValue(0).ToString();
        }

        public void GetTotalTransaction(int strTerminal)
        {
            RMQueries("SELECT count(*) FROM  SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo);
            dbTotalTransaction = rdr.GetValue(0).ToString();
        }

        #endregion

        #region "Save Computation to DBF"

        public void SaveComputeToDBF(string ServiceCharge ) 
        {
           DBFQuery("INSERT INTO day.dbf VALUES (" + ServiceCharge + ")");
        }
        
        #endregion

        public void taxNo(int taxNo)
        { 
            Queries("SELECT tblTaxMapping WHERE TaxMap = "+taxNo+" ");
        }

    }
}
