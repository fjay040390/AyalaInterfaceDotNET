using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet.ClassCon
{
    public class classReprocess : classQuery
    {
     
        public classReprocess()
        {
            LoadConfigDetails();
            CheckFolderAyala();
            LoadDiscount();
            LoadTaxMap();
            LoadLessVAT();
            GetTaxTableConfig();
            GetLessVatConfig();
        }

        #region "Declaration"

        public string repYear { get; set; }
        public string repMonth { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string sessNo { get; set; }
        public string bill_start { get; set; }
        public string bill_end { get; set; }

        public DateTime strDate { get; set; }
        public DateTime endDate { get; set; }

        public string fileNamePath { get; set; }
        public string distinationFolder { get; set; }
        public string distinationFileName { get; set; }
        public string hourlyPath { get; set; }
        public string sourceFile { get; set; }
        public string targetFile { get; set; }
        #endregion

        #region "Properties for Computation"

        public int dbTotalTransaction { get; set; }
        public int TerminalNumber { get; set; }

        public double dbServiceCharge { get; set; }
        public double dbNoTaxSales { get; set; }
        public double dbLocalTax { get; set; }
        public double dbTotalOthers { get; set; }
        public double dbTotalSales { get; set; }
        public double dbTotalVat { get; set; }
        public double dbTotalCAN { get; set; }
        public double dbTotalREF { get; set; }
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

        public double dbhrlySales { get; set; }
        public double dbhrlyTotalTransaction { get; set; }
        public string transactionHours { get; set; }

        public string strDscVat {get; set; }
        public string TenantName { get; set; }

        #endregion

        #region "Tax Table Fields"

        public string primaryVAT, secondaryVAT;
        public string primarySC, secondarySC;
        public string primaryOthers1, secondaryOthers1;
        public string primaryOthers2, secondaryOthers2;
        public string primaryOthers3, secondaryOthers3;

        #endregion

        #region "Save Filter"

        //FilteringDate
        public void FilterDate()
        {
            bill_start = "";
            bill_end = "";
            endDate = Convert.ToDateTime(dateEnd);
            strDate = Convert.ToDateTime(dateStart);
            do {
                //frmReprocess frmReprocess = new frmReprocess();
                //frmReprocess.lblReprocessDate = dateStart;
                //Connect to DB
                rmConnect();
                //2016 = 16
                repYear = strDate.ToString("yy");
                //January = 01
                repMonth = strDate.ToString("MM");
                //GetSession
                GetSessionNo();
                //Get Sales
                ComputeDailySales(1);
                //Get Hourly Sales                
                ComputeHourlySales("00:00:00","00:59:59",1);
                ComputeHourlySales("01:00:00", "01:59:59", 1);
                ComputeHourlySales("02:00:00", "02:59:59", 1);
                ComputeHourlySales("03:00:00", "03:59:59", 1);
                ComputeHourlySales("04:00:00", "04:59:59", 1);
                ComputeHourlySales("05:00:00", "05:59:59", 1);
                ComputeHourlySales("06:00:00", "06:59:59", 1);
                ComputeHourlySales("07:00:00", "07:59:59", 1);
                ComputeHourlySales("08:00:00", "08:59:59", 1);
                ComputeHourlySales("09:00:00", "09:59:59", 1);
                ComputeHourlySales("10:00:00", "10:59:59", 1);
                ComputeHourlySales("11:00:00", "11:59:59", 1);
                ComputeHourlySales("12:00:00", "12:59:59", 1);
                ComputeHourlySales("13:00:00", "13:59:59", 1);
                ComputeHourlySales("14:00:00", "14:59:59", 1);
                ComputeHourlySales("15:00:00", "15:59:59", 1);
                ComputeHourlySales("16:00:00", "16:59:59", 1);
                ComputeHourlySales("17:00:00", "17:59:59", 1);
                ComputeHourlySales("18:00:00", "18:59:59", 1);
                ComputeHourlySales("19:00:00", "19:59:59", 1);
                ComputeHourlySales("20:00:00", "20:59:59", 1);
                ComputeHourlySales("21:00:00", "21:59:59", 1);
                ComputeHourlySales("22:00:00", "22:59:59", 1);
                ComputeHourlySales("23:00:00", "23:59:59", 1);
                //Write to file
                //classWriteFile writeFile = new classWriteFile();
                //writeFile.GenerateFile();
                //Add 1 day to loop
                strDate = strDate.AddDays(1);
                //Disconnect to DB
                rmDisconnect();
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
            //classWriteFile classWriteFile = new classWriteFile();
            
            //BeginOr, EndOR, BeginInv, EndInv
            bill_startINV = bill_start;
            bill_endINV = bill_end;
            //terminal Number
            TerminalNumber = strTerminal;
            //transaction Date
            dbDateStart = strDate.ToString("d");
                    
            //TotalVat
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Pay_Type <> 5 AND  b.Session_no = " + sessNo + " AND a.Tax_No = " + primaryVAT + " OR a.Tax_No = " + secondaryVAT + " AND b.Settle_stn =" + strTerminal);
            dbTotalVat = ReturnData(rdr[0].ToString());

            //Total Discount
            //CHECK
            RMQueries("SELECT Sum(a.Discount) as TotalPercentCheck FROM SLS" + repMonth + repYear + ".DBF a WHERE a.Session_No = " + sessNo + " AND a.pay_type <> 5 AND a.disc_type NOT IN (" + strDscVat + ")  AND a.settle_stn=" + strTerminal);
            dbTotalPercentCheck = ReturnData(rdr[0].ToString()); 
            //ITEM
            RMQueries("SELECT SUM(ABS(a.ITEM_ADJ) * a.QUANTY) AS TotalPercentItem FROM SDET" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + ".DBF b ON a.Bill_No = b.Bill_No WHERE b.pay_type <> 5 and b.Session_No = " + sessNo + " AND a.DISC_NO NOT IN (" + strDscVat + ") and b.settle_stn =" + strTerminal);
            dbTotalPercentItem = ReturnData(rdr[0].ToString());
            //Total Discounts
            dbTotalDiscount = dbTotalPercentItem + dbTotalPercentCheck;

            //totalCan
            RMQueries("SELECT SUM(TOTAL) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND Pay_type = 5 AND Settle_stn =" + strTerminal);
            dbTotalCAN = ReturnData(rdr[0].ToString());
                          
            //totalRef
            dbTotalREF = 0;

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
            dbTotalRawGross = dbTotalSales + dbLocalTax + dbTotalVat + dbServiceCharge + dbTotalCAN + dbTotalDiscount;
            
            //Total Daily Sales
            dbTotalDlySales = dbTotalRawGross - dbTotalDiscount - dbTotalCAN - dbServiceCharge - dbTotalVat;

            //Old Grand Total
            Queries("SELECT TOP 1 GTAmount as GTAmnt FROM tblGTValues WHERE SessionNo <= " + sessNo + " AND TerminalID = '" + strTerminal + "' ORDER BY SessionNO DESC");
            dbOldGT = Convert.ToDouble(rdr["GTAmnt"].ToString());
                                     
            //New Grand Total
            dbNewGT = dbOldGT + dbTotalDlySales + dbTotalVat;
            GenerateDailyFile();
            //classWriteFile.GenerateFile(dbDateStart);
            
        }

        public void ComputeHourlySales(string startHour, string endHours, Int16 strTerminal)
        {
   
            RMQueries("SELECT Sum(Total + Taxes) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo +
                      " AND Pay_Type <> 5 AND Open_Time >= '" + startHour + "' AND Open_Time <= '" + endHours +
                      "' AND Settle_stn = " + strTerminal);
            dbhrlySales = ReturnData(rdr[0].ToString());

            RMQueries("SELECT Count(bill_no)FROM SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo +
                     "AND Open_Time >= '" + startHour + "' AND Open_Time <= '" + endHours + "' AND Settle_stn = " + strTerminal);
            dbhrlyTotalTransaction = ReturnData(rdr[0].ToString());

            GenerateHourlyFile("00:00");
            GenerateHourlyFile("01:00");
            GenerateHourlyFile("02:00");
            GenerateHourlyFile("03:00");
            GenerateHourlyFile("04:00");
            GenerateHourlyFile("05:00");
            GenerateHourlyFile("06:00");
            GenerateHourlyFile("07:00");
            GenerateHourlyFile("08:00");
            GenerateHourlyFile("09:00");
            GenerateHourlyFile("10:00");
            GenerateHourlyFile("11:00");
            GenerateHourlyFile("12:00");
            GenerateHourlyFile("13:00");
            GenerateHourlyFile("14:00");
            GenerateHourlyFile("15:00");
            GenerateHourlyFile("16:00");
            GenerateHourlyFile("17:00");
            GenerateHourlyFile("18:00");
            GenerateHourlyFile("19:00");
            GenerateHourlyFile("20:00");
            GenerateHourlyFile("21:00");
            GenerateHourlyFile("22:00");
            GenerateHourlyFile("23:00");

        }

        #endregion

        #region "Write Files"

        public void GenerateDailyFile()
        {
            TemplateConnection();
            DBFQuery("INSERT INTO Daily VALUES ('" + dbDateStart + "'," + dbOldGT + "," + dbNewGT + "," + dbTotalDlySales + "," + dbTotalDiscount + "," + dbTotalREF + "," + dbTotalCAN + "," + dbTotalVat + ",'" + tenantName + "'," + bill_startINV + "," + bill_endINV + "," + bill_start + "," + bill_end + "," + dbTotalTransaction + "," + dbLocalTax + "," + dbServiceCharge + "," + dbNoTaxSales + "," + dbTotalRawGross + "," + dbLocalTax + "," + dbTotalOthers + "," + TerminalNumber + ")");
            TemplateConnectionClose();
        }
        public void GenerateHourlyFile(string hours)
        {
            TemplateConnection();
            DBFQuery("INSERT INTO Hourly Values ('" + dbDateStart + "','" + hours +"', " + dbhrlySales + ", " + dbhrlyTotalTransaction +  ",'" + tenantName + "'," + TerminalNumber + ")");
            TemplateConnectionClose();
        }

        #endregion  

        #region "SavingFile"

        public void CopyFileToAyalaFolder()
        {
           
            fileNamePath = System.IO.Directory.GetCurrentDirectory() + "\\Template\\";
            hourlyPath = "hourly.DBF";
            distinationFileName = tenantCode + DateTime.Now.ToString ("yydd") + "H" + ".DBF";
            sourceFile = System.IO.Path.Combine(fileNamePath,hourlyPath);
            //distinationFolder = System.IO.Path.Combine(ayalaFolderPath, distinationFileName);
            File.Copy(ayalaFolderPath, sourceFile);
        }
        #endregion

    }
}
