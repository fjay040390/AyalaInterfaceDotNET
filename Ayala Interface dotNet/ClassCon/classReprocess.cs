using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;

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

        #region "Properties for Computation"

        public Int64 dbTotalTransaction { get; set; }

        public double dbServiceCharge { get; set; }
        public double dbNoTaxSales { get; set; }
        public double dbLocalTax { get; set; }
        public double dbTotalOthers { get; set; }
        public double dbTotalSales { get; set; }
        public double dbTotalVat { get; set; }
        public double dbTotalCANREF { get; set; }
        public double dbTotalDiscount { get; set; }
        public double dbTotalPercentCheck { get; set; }
        public double dbTotalPercentItem { get; set; }
        
        public string dbDateStart { get; set; }
        public string bill_startINV { get; set; }
        public string bill_endINV { get; set; }
        
        public double dbTotalRawGross { get; set; }
        public double dbTotalDlySales { get; set; }
        
        public double dbOldGT { get; set; }
        public double dbNewGT { get; set; }

        public string strDscVat {get; set; }
        #endregion

        #region "Tax Table Fields"

        public string primaryVAT, secondaryVAT;
        public string primarySC, secondarySC;
        public string primaryOthers1, secondaryOthers1;
        public string primaryOthers2, secondaryOthers2;
        public string primaryOthers3, secondaryOthers3;

        #endregion

        public classReprocess()
        {
            LoadConfigDetails();
            LoadDiscount();
            LoadTaxMap();
            LoadLessVAT();
            GetTaxTableConfig();
            GetLessVatConfig();
            rmConnect();
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
                //2016 = 16
                repYear = strDate.ToString("yy");
                //January = 01
                repMonth = strDate.ToString("MM");
                //GetSession
                GetSessionNo();
                //Get Sales
                ComputeDailySales(1);
                //Write to file
                //classWriteFile writeFile = new classWriteFile();
                //writeFile.GenerateFile();
                //Add 1 day to loop
                strDate = strDate.AddDays(1);
            } while (strDate <= endDate);
        }
        #endregion

        #region "Utility Functions"

        //Select QUERY from REP
        public void GetSessionNo()
        {
           RMQueries("SELECT session_no,first_bill,last_bill FROM REP" + repYear + ".DBF WHERE date_start = #" + strDate + "#");
           sessNo = rdr.GetValue(0).ToString();
           bill_start = rdr.GetValue(1).ToString();
           bill_end = rdr.GetValue(2).ToString();
        }

        //Save data from query REP
        public void SaveFilterDate(string str_date, string bill_start, string bill_end)
        {
            Queries("INSERT INTO tblFilterDate VALUES ('" + str_date + "'," + bill_start + "," + bill_end + ",'" + repMonth + "','" + repYear + "')");
        }

        //Load Tax Table Config
        public void GetTaxTableConfig()
        {
            //Tax
            primaryVAT = dtTax.Rows[0]["PLU"].ToString();
            secondaryVAT = dtTax.Rows[5]["PLU"].ToString();

            //Service Charge
            primarySC = dtTax.Rows[1]["PLU"].ToString();
            secondarySC = dtTax.Rows[6]["PLU"].ToString(); 

            //Others 1
            primaryOthers1 = dtTax.Rows[2]["PLU"].ToString();
            secondaryOthers1 = dtTax.Rows[7]["PLU"].ToString();

            //Others 2
            primaryOthers2 = dtTax.Rows[3]["PLU"].ToString();
            secondaryOthers2 = dtTax.Rows[8]["PLU"].ToString();
            
            //Others 3
            primaryOthers3 = dtTax.Rows[4]["PLU"].ToString();
            secondaryOthers3 = dtTax.Rows[9]["PLU"].ToString();
        }

        //Load LESSVAT table Config
        public void GetLessVatConfig() 
        {
            if (dtLessVAT.Rows.Count > 0) 
            {
                DataColumn col = dtLessVAT.Columns["PLU"];
                foreach (DataRow row in dtLessVAT.Rows) {
                    strDscVat = strDscVat + row[col].ToString() + ",";
                }
                strDscVat = strDscVat.Remove(strDscVat.Length - 1);
            }
        }

        //To check if data from database is empty, return 0. 
        public double ReturnData(string param)
        {
            if (param != "")
            {
                return Convert.ToDouble(param);
            }
            else
            {
                return 0.0;
            }
        }
        #endregion

        #region "Computation"

        public void ComputeDailySales(int strTerminal)
        {
            //BeginOr, EndOR, BeginInv, EndInv
            bill_startINV = bill_start;
            bill_endINV = bill_end;
            
            //transaction Date
            dbDateStart = dateStart;

            //TotalVat
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Pay_Type <> 5 AND  b.Session_no = " + sessNo + " AND a.Tax_No = " + primaryVAT + " OR a.Tax_No = " + secondaryVAT + " AND b.Settle_stn =" + strTerminal);
            dbTotalVat = ReturnData(rdr[0].ToString());

            //Total Discount
            //CHECK
            RMQueries("SELECT Sum(a.Discount) as TotalPercentCheck FROM SLS" + repMonth + repYear + ".DBF a WHERE a.Session_No = " + sessNo + " AND a.pay_type <> 5 AND a.disc_type NOT IN (" + strDscVat + ")  AND a.settle_stn=" + strTerminal);
            dbTotalPercentCheck = ReturnData(rdr[0].ToString()); 
            //ITEM
            RMQueries("SELECT SUM(ABS(ITEM_ADJ) * QUANTY) AS TotalPercentItem FROM SDET" + repMonth + repYear + " LEFT JOIN SLS" + repMonth + repYear + " a ON Bill_No = Bill_No WHERE pay_type <> 5 and Session_No = " + sessNo + " AND DISC_NO NOT IN(" + strDscVat + ") and settle_stn =" + strTerminal);
            dbTotalPercentItem = ReturnData(rdr[0].ToString());
            //Total Discounts
            dbTotalDiscount = dbTotalPercentItem + dbTotalPercentCheck;

            //totalCanRef
            RMQueries("SELECT SUM(TOTAL) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND Pay_type = 5 AND Settle_stn =" + strTerminal);
            dbTotalCANREF = ReturnData(rdr[0].ToString());
                                   
            //getServiceCharge
            //Auto Grat
            RMQueries("SELECT sum(auto_grat) from SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo);
            dbServiceCharge = ReturnData(rdr[0].ToString());

            //Tax table
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Pay_Type <> 5 AND  b.Session_no = " + sessNo + " AND a.Tax_No = " + primarySC + " OR a.Tax_No = " + secondarySC + " AND b.Settle_stn =" + strTerminal);
            dbServiceCharge = dbServiceCharge + ReturnData(rdr[0].ToString()); 

            //Total Sales
            RMQueries("SELECT Sum(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND pay_type <> 5 AND Settle_stn = " + strTerminal);
            dbTotalSales = ReturnData(rdr[0].ToString());

            //getNoTaxSales
            RMQueries("SELECT Sum(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND pay_type <> 5 AND Settle_stn = " + strTerminal + " AND NOT Taxable");
            dbNoTaxSales = ReturnData(rdr[0].ToString());

            //GetLocalTax
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Pay_Type <> 5 AND  b.Session_no = " + sessNo + " AND a.Tax_No = " + primaryOthers1 + " OR a.Tax_No = " + secondaryOthers1 + " AND b.Settle_stn =" + strTerminal);
            dbLocalTax = ReturnData(rdr[0].ToString());

            //getTotalOthers
            RMQueries("SELECT SUM(People_no) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo + " AND Pay_type <> 5 and Settle_stn = " + strTerminal);
            dbTotalOthers = ReturnData(rdr[0].ToString());

            //getTotalTransaction
            dbTotalTransaction = (int.Parse(bill_end) - int.Parse(bill_start)) + 1;
           
            //Total Raw Gross
            dbTotalRawGross = dbTotalSales + dbLocalTax + dbTotalVat + dbServiceCharge + dbTotalCANREF + dbTotalDiscount;
            
            //Total Daily Sales
            dbTotalDlySales = dbTotalRawGross - dbTotalDiscount - dbTotalCANREF - dbServiceCharge - dbTotalVat;

            //Old Grand Total
            Queries("SELECT TOP 1 GTAmount as GTAmnt FROM tblGTValues WHERE SessionNo <= " + sessNo + " AND TerminalID = '" + strTerminal + "' ORDER BY SessionNO DESC");
            dbOldGT = Convert.ToDouble(rdr["GTAmnt"].ToString());
                                     
            //New Grand Total
            dbNewGT = dbOldGT + dbTotalDlySales + dbTotalVat;
        }

        #endregion

    }
}
