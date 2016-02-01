using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet.ClassCon
{
    public class classReprocess : classQuery
    {
        public classReprocess()
        {
            LoadConfigDetails();
            LoadDiscount();
            LoadTaxMap();
            LoadLessVAT();
            LoadDiplomat();
            LoadTerminal();
            GetTaxTableConfig();
            GetLessVatConfig();
            GetSeniorConfig();
            GetDiplomatConfig();
        }

        #region "Declaration"

        public string repYear { get; set; }
        public string repMonth { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public int sessNo { get; set; }
        public int bill_start { get; set; }
        public int bill_end { get; set; }

        public DateTime strDate { get; set; }
        public DateTime endDate { get; set; }

        public string fileNamePath { get; set; }
        public string distinationFolder { get; set; }
        public string hourlyPath { get; set; }
        public string dailyPath { get; set; }
        public string hourlyDBFFilename { get; set; }
        public string dailyDBFFileName { get; set; }
        public string dbfFileName { get; set; }
        
        public StreamWriter hourlyTextfile;
        public StreamWriter dailyTextfile;
        public int strTerminalID;

        public int strTerminal2;
        #endregion

        #region "Properties for Computation"

        public int dbTotalTransaction { get; set; }
        public int dbTotalNonVoidCount { get; set; }
        public int TerminalNumber { get; set; }

        public double dbServiceCharge { get; set; }
        public double dbNoTaxSales { get; set; }
        public double dbTaxSales { get; set; }
        public double dbLocalTax { get; set; }
        public double dbTotalOthers { get; set; }
        public double dbTotalSales { get; set; }
        public double dbTotalVat { get; set; }
        public double dbTotalCAN { get; set; }
        public double dbTotalREF { get; set; }
        public double dbTotalSenior { get; set; }
        public double dbTotalDiplomat { get; set; }
        public double dbTotalVatExempt { get; set; }
        public double dbTotalDiscount { get; set; }
        public double dbTotalPercentCheck { get; set; }
        public double dbTotalPercentItem { get; set; }
        
        public string dbDateStart { get; set; }
        public Int64 bill_startINV { get; set; }
        public Int64 bill_endINV { get; set; }
        
        public double dbTotalRawGross { get; set; }
        public double dbTotalDlySales { get; set; }
        
        public double dbOldGT { get; set; }
        public double dbRunningGT { get; set; }
        public double dbNewGT { get; set; }

        public double dbhrlySales { get; set; }
        public double dbhrlyTotalTransaction { get; set; }
        public string transactionHours { get; set; }

        public string strDscVat {get; set; }
        public string strDscSC { get; set; }
        public string strDscDiplomat { get; set; }
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
            strTerminal2 = 1;
            endDate = Convert.ToDateTime(dateEnd);
            strDate = Convert.ToDateTime(dateStart);
            GetOldGT(1, sessNo);
            do {
                //Check if Ayala Folder Exists
                CheckFolderAyala(strDate.ToString("yyyy"));
                //Filename
                dbfFileName = tenantCode + strDate.ToString("MMdd");
                //Connect to DB
                rmConnect();
                openConnection();
                //2016 = 16
                repYear = strDate.ToString("yy");
                //January = 01
                repMonth = strDate.ToString("MM");
                //GetSession
                GetSessionNo();              
                //Copy Template to Ayala
                CopyFileHourlyToAyalaFolder();
                CopyFileDailyToAyalaFolder();
                //Write Textfile Headers
                hourlyTextfile = new StreamWriter(ayalaFolder + "\\" + strDate.ToString("yyyy") + "\\" + tenantContract + tenantCode + strDate.ToString("MMdd") + "H.txt");
                hourlyTextfile.WriteLine("TRANDATE,HOUR,SALES,TRANCNT,TENTNAME,TERMNUM");
                dailyTextfile = new StreamWriter(ayalaFolder + "\\" + strDate.ToString("yyyy") + "\\" + tenantContract + tenantCode + strDate.ToString("MMdd") + ".txt");
                dailyTextfile.WriteLine("TRANDATE,OLDGT,NEWGT,DLYSALE,TOTDISC,TOTREF,TOTCAN,VAT,TENTNAME,BEGINV,ENDINV,BEGOR,ENDOR,TRANCNT,LOCALTX,SERVCHARGE,NOTAXSALE,RAWGROSS,DLYLOCTAX,OTHERS,TERMNUM");
                
                foreach (DataRow row in dtTerminal.Rows)
                {
                    //terminal 1
                    strTerminalID = Convert.ToInt16(row[0].ToString());
                    //Get Sales
                    ComputeDailySales(strTerminalID);
                    //Get Hourly Sales
                    ComputeHourlySales("06:00:00", "06:59:59", strTerminalID);
                    GenerateHourlyDBFFile("6:00", dbfFileName);
                    GenerateHourlyTextFile("6:00");
                    ComputeHourlySales("07:00:00", "07:59:59", strTerminalID);
                    GenerateHourlyDBFFile("7:00", dbfFileName);
                    GenerateHourlyTextFile("7:00");
                    ComputeHourlySales("08:00:00", "08:59:59", strTerminalID);
                    GenerateHourlyDBFFile("8:00", dbfFileName);
                    GenerateHourlyTextFile("8:00");
                    ComputeHourlySales("09:00:00", "09:59:59", strTerminalID);
                    GenerateHourlyDBFFile("9:00", dbfFileName);
                    GenerateHourlyTextFile("9:00");
                    ComputeHourlySales("10:00:00", "10:59:59", strTerminalID);
                    GenerateHourlyDBFFile("10:00", dbfFileName);
                    GenerateHourlyTextFile("10:00");
                    ComputeHourlySales("11:00:00", "11:59:59", strTerminalID);
                    GenerateHourlyDBFFile("11:00", dbfFileName);
                    GenerateHourlyTextFile("11:00");
                    ComputeHourlySales("12:00:00", "12:59:59", strTerminalID);
                    GenerateHourlyDBFFile("12:00", dbfFileName);
                    GenerateHourlyTextFile("12:00");
                    ComputeHourlySales("13:00:00", "13:59:59", strTerminalID);
                    GenerateHourlyDBFFile("13:00", dbfFileName);
                    GenerateHourlyTextFile("13:00");
                    ComputeHourlySales("14:00:00", "14:59:59", strTerminalID);
                    GenerateHourlyDBFFile("14:00", dbfFileName);
                    GenerateHourlyTextFile("14:00");
                    ComputeHourlySales("15:00:00", "15:59:59", strTerminalID);
                    GenerateHourlyDBFFile("15:00", dbfFileName);
                    GenerateHourlyTextFile("15:00");
                    ComputeHourlySales("16:00:00", "16:59:59", strTerminalID);
                    GenerateHourlyDBFFile("16:00", dbfFileName);
                    GenerateHourlyTextFile("16:00");
                    ComputeHourlySales("17:00:00", "17:59:59", strTerminalID);
                    GenerateHourlyDBFFile("17:00", dbfFileName);
                    GenerateHourlyTextFile("17:00");
                    ComputeHourlySales("18:00:00", "18:59:59", strTerminalID);
                    GenerateHourlyDBFFile("18:00", dbfFileName);
                    GenerateHourlyTextFile("18:00");
                    ComputeHourlySales("19:00:00", "19:59:59", strTerminalID);
                    GenerateHourlyDBFFile("19:00", dbfFileName);
                    GenerateHourlyTextFile("19:00");
                    ComputeHourlySales("20:00:00", "20:59:59", strTerminalID);
                    GenerateHourlyDBFFile("20:00", dbfFileName);
                    GenerateHourlyTextFile("20:00");
                    ComputeHourlySales("21:00:00", "21:59:59", strTerminalID);
                    GenerateHourlyDBFFile("21:00", dbfFileName);
                    GenerateHourlyTextFile("21:00");
                    ComputeHourlySales("22:00:00", "22:59:59", strTerminalID);
                    GenerateHourlyDBFFile("22:00", dbfFileName);
                    GenerateHourlyTextFile("22:00");
                    ComputeHourlySales("23:00:00", "23:59:59", strTerminalID);
                    GenerateHourlyDBFFile("23:00", dbfFileName);
                    GenerateHourlyTextFile("23:00");
                    ComputeHourlySales("00:00:00", "00:59:59", strTerminalID);
                    GenerateHourlyDBFFile("0:00", dbfFileName);
                    GenerateHourlyTextFile("0:00");
                    ComputeHourlySales("01:00:00", "01:59:59", strTerminalID);
                    GenerateHourlyDBFFile("1:00", dbfFileName);
                    GenerateHourlyTextFile("1:00");
                    ComputeHourlySales("02:00:00", "02:59:59", strTerminalID);
                    GenerateHourlyDBFFile("2:00", dbfFileName);
                    GenerateHourlyTextFile("2:00");
                    ComputeHourlySales("03:00:00", "03:59:59", strTerminalID);
                    GenerateHourlyDBFFile("3:00", dbfFileName);
                    GenerateHourlyTextFile("3:00");
                    ComputeHourlySales("04:00:00", "04:59:59", strTerminalID);
                    GenerateHourlyDBFFile("4:00", dbfFileName);
                    GenerateHourlyTextFile("4:00");
                    ComputeHourlySales("05:00:00", "05:59:59", strTerminalID);
                    GenerateHourlyDBFFile("5:00", dbfFileName);
                    GenerateHourlyTextFile("5:00");   
             
                    //Terminal 2
                    TXTHourlyTerminal2();
                    DBFHourlyTerminal2(dbfFileName);
                }
            
                hourlyTextfile.Close();
                dailyTextfile.Close();
                Terminal2Reports();
                //Write Report5
                GenerateDailyReport(printerPath, "Report " + strDate.ToString("MM-dd-yyyy") + ".spl");
                //Add 1 day to loop
                strDate = strDate.AddDays(1);
                strTerminal2++;
                //Disconnect to DB
                rmDisconnect();
                closeConnection();
            } while (strDate <= endDate);
            
        }
        
        #endregion

        #region "Utility Functions"

        //Select QUERY from REP
        public void GetSessionNo()
        {
           RMQueries("SELECT session_no,first_bill,last_bill FROM REP" + repYear + ".DBF WHERE date_start = #" + strDate + "#");
           sessNo = Convert.ToInt16(rdr.GetValue(0).ToString());
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

        //Load Senior table Config
        public void GetSeniorConfig()
        {
            if (dtDiscount.Rows.Count > 0)
            {
                DataColumn col = dtDiscount.Columns["PLU"];
                foreach (DataRow row in dtDiscount.Rows){
                    strDscSC = strDscSC + row[col].ToString() + ",";
                }
                strDscSC = strDscSC.Remove(strDscSC.Length - 1);
            }
        }

        //Load Diplomat Config
        public void GetDiplomatConfig()
        {
            if (dtDiplomat.Rows.Count > 0)
            {
                DataColumn col = dtDiplomat.Columns["PLU"];
                foreach (DataRow row in dtDiplomat.Rows)
                {
                    strDscDiplomat = strDscDiplomat + row[col].ToString() + ",";
                }
                strDscDiplomat = strDscDiplomat.Remove(strDscDiplomat.Length - 1);
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

        //Get Old Grand Total

        public void GetOldGT(int strTerminal, int sessionNo)
        {
            //2016 = 16
            repYear = strDate.ToString("yy");

            rmConnect();
            GetSessionNo();

            LoadOldGrandTotal("SELECT top 1 sessionNo, GTValues FROM tblGTValues WHERE SessionNo < " + sessionNo +
            " AND TerminalID = '" + strTerminal + "' ORDER BY SessionNo DESC");
            DataRow row;
            if (dtGrandTotal.Rows.Count > 0)
            {
                row = dtGrandTotal.Rows[0];
                dbOldGT = Convert.ToDouble(row["GTValues"].ToString());
                dbRunningGT = dbOldGT;
            }

            rmDisconnect();
        }

        #endregion

        #region "Computation"

        public void ComputeDailySales(int strTerminal)
        {
            //BeginOr, EndOR, BeginInv, EndInv
            RMQueries("Select MIN(bill_no), MAX(bill_no) from SLS" + repMonth + repYear + ".DBF where session_no =" + sessNo + " and settle_stn =" + strTerminal);
            bill_start = Convert.ToInt16(ReturnData(rdr[0].ToString()));
            bill_end = Convert.ToInt16(ReturnData(rdr[1].ToString()));
            bill_startINV = bill_start;
            bill_endINV = bill_end;

            //terminal Number
            TerminalNumber = strTerminal;
            
            //transaction Date
            dbDateStart = strDate.ToString("d");
                    
            //TotalVat
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Session_no = " + sessNo + " AND b.Settle_stn =" + strTerminal + " AND b.Pay_Type <> 5  AND a.Tax_No = " + primaryVAT + " OR a.Tax_No = " + secondaryVAT ); 
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

            //Get Senior Citizen Discount
            //Check
            RMQueries("SELECT Sum(a.Discount) as TotalPercentCheck FROM SLS" + repMonth + repYear + ".DBF a WHERE a.Session_No = " + sessNo + " AND a.pay_type <> 5 AND a.disc_type IN (" + strDscSC + ")  AND a.settle_stn=" + strTerminal);
            dbTotalSenior = ReturnData(rdr[0].ToString());
            //ITEM
            RMQueries("SELECT SUM(ABS(a.ITEM_ADJ) * a.QUANTY) AS TotalPercentItem FROM SDET" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + ".DBF b ON a.Bill_No = b.Bill_No WHERE b.pay_type <> 5 and b.Session_No = " + sessNo + " AND a.DISC_NO IN (" + strDscSC + ") and b.settle_stn =" + strTerminal);
            dbTotalSenior = dbTotalSenior + ReturnData(rdr[0].ToString());

            //Get Diplomat
            //Check
            RMQueries("SELECT Sum(a.Discount) as TotalPercentCheck FROM SLS" + repMonth + repYear + ".DBF a WHERE a.Session_No = " + sessNo + " AND a.pay_type <> 5 AND a.disc_type IN (" + strDscDiplomat + ")  AND a.settle_stn=" + strTerminal);
            dbTotalDiplomat = ReturnData(rdr[0].ToString());
            //ITEM
            RMQueries("SELECT SUM(ABS(a.ITEM_ADJ) * a.QUANTY) AS TotalPercentItem FROM SDET" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + ".DBF b ON a.Bill_No = b.Bill_No WHERE b.pay_type <> 5 and b.Session_No = " + sessNo + " AND a.DISC_NO IN (" + strDscDiplomat + ") and b.settle_stn =" + strTerminal);
            dbTotalDiplomat = dbTotalDiplomat + ReturnData(rdr[0].ToString());

            //Get VAT Exempt
            //Check
            RMQueries("SELECT Sum(a.Discount) as TotalPercentCheck FROM SLS" + repMonth + repYear + ".DBF a WHERE a.Session_No = " + sessNo + " AND a.pay_type <> 5 AND a.disc_type IN (" + strDscVat + ")  AND a.settle_stn=" + strTerminal);
            dbTotalVatExempt = ReturnData(rdr[0].ToString());
            //ITEM
            RMQueries("SELECT SUM(ABS(a.ITEM_ADJ) * a.QUANTY) AS TotalPercentItem FROM SDET" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + ".DBF b ON a.Bill_No = b.Bill_No WHERE b.pay_type <> 5 and b.Session_No = " + sessNo + " AND a.DISC_NO IN (" + strDscVat + ") and b.settle_stn =" + strTerminal);
            dbTotalVatExempt = dbTotalVatExempt + ReturnData(rdr[0].ToString());

            //Get Total Cancelled (Void)
            RMQueries("SELECT SUM(TOTAL) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND Pay_type = 5 AND Settle_stn =" + strTerminal);
            dbTotalCAN = ReturnData(rdr[0].ToString());

            //Get Total Non Void Count
            RMQueries("SELECT count(bill_NO) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND Pay_type <> 5 AND Settle_stn =" + strTerminal);
            dbTotalNonVoidCount = rdr.GetInt32(0);
  
            //totalRef
            dbTotalREF = 0;

            //getServiceCharge
            //Auto Grat
            RMQueries("SELECT sum(auto_grat) from SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + "  AND Settle_stn =" + strTerminal + " AND Pay_type <> 5");
            dbServiceCharge = ReturnData(rdr[0].ToString());

            //Tax table
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Pay_Type <> 5 AND  b.Session_no = " + sessNo + " AND a.Tax_No = " + primarySC + " OR a.Tax_No = " + secondarySC + " AND b.Settle_stn =" + strTerminal);
            dbServiceCharge = dbServiceCharge + ReturnData(rdr[0].ToString()); 

            //Total Sales
            RMQueries("SELECT Sum(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND pay_type <> 5 AND Settle_stn = " + strTerminal);
            dbTotalSales = ReturnData(rdr[0].ToString());

            //Taxable Sales
            RMQueries("SELECT Sum(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND pay_type <> 5 AND Settle_stn = " + strTerminal + " AND Taxable");
            dbTaxSales = ReturnData(rdr[0].ToString());

            //Non Taxable Sales
            RMQueries("SELECT Sum(Total) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo + " AND pay_type <> 5 AND Settle_stn = " + strTerminal + " AND NOT Taxable");
            dbNoTaxSales = ReturnData(rdr[0].ToString());

            //GetLocalTax
            RMQueries("SELECT SUM(a.Tax_Amt) FROM TAX" + repMonth + repYear + ".DBF a LEFT JOIN SLS" + repMonth + repYear + " b ON b.Bill_No = a.Bill_No WHERE b.Pay_Type <> 5  AND  b.Session_no = " + sessNo + " AND a.Tax_No = " + primaryOthers1 + " OR a.Tax_No = " + secondaryOthers1 + " AND b.Settle_stn =" + strTerminal + "");
            dbLocalTax = ReturnData(rdr[0].ToString());
            
            //getTotalOthers
            RMQueries("SELECT SUM(People_no) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo + " and Settle_stn = " + strTerminal + "  AND Pay_type <> 5");
            dbTotalOthers = ReturnData(rdr[0].ToString());

            //getTotalTransaction
            if (bill_end == 0 && bill_start == 0)
            {
                dbTotalTransaction = 0;
            }
            else 
            {
                dbTotalTransaction = (bill_end - bill_start) + 1;
            }
                       
            //Total Raw Gross
            dbTotalRawGross = dbTotalSales + dbLocalTax + dbTotalVat + dbServiceCharge + dbTotalCAN + dbTotalDiscount;
            
            //Total Daily Sales
            dbTotalDlySales = dbTotalRawGross - dbTotalDiscount - dbTotalCAN - dbServiceCharge - dbTotalVat;
                                   
            //New Grand Total
            dbNewGT = dbRunningGT + dbTotalDlySales + dbTotalVat;
            Queries("Select * from tblGTValues where sessionNo =" + sessNo);
            if (rdr.HasRows == false)
            {
                Queries("INSERT INTO tblGTValues (sessionNo,TerminalID,GTValues) values(" + sessNo + "," + strTerminal + "," + dbNewGT + ")");
            }

            //Generate DBF File
            GenerateDailyDBFFile(dbfFileName);
            //Generate Textfile
            GenerateDailyTextFile();
            TXTDailyTerminal2();
            dbRunningGT = dbNewGT;
        }

        public void ComputeHourlySales(string startHour, string endHours, int strTerminal)
        {
            RMQueries("SELECT Sum(Total + Taxes) FROM SLS" + repMonth + repYear + ".DBF WHERE Session_No = " + sessNo +
                      " AND Pay_Type <> 5 AND Open_Time >= '" + startHour + "' AND Open_Time <= '" + endHours +
                      "' AND Settle_stn = " + strTerminal);
            dbhrlySales = ReturnData(rdr[0].ToString());

            RMQueries("SELECT Count(bill_no)FROM SLS" + repMonth + repYear + ".DBF WHERE Session_no = " + sessNo +
                     "AND Open_Time >= '" + startHour + "' AND Open_Time <= '" + endHours + "' AND Settle_stn = " + strTerminal);
            dbhrlyTotalTransaction = ReturnData(rdr[0].ToString());
        }

        #endregion

        #region "Write Files"

        public void GenerateDailyDBFFile(string fileName)
        {
            TemplateConnection(ayalaFolder + "\\" + strDate.ToString("yyyy"));
            DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('" + dbDateStart + "'," + dbRunningGT + "," + dbNewGT + "," + dbTotalDlySales + "," + dbTotalDiscount + "," + dbTotalREF + "," + dbTotalCAN + "," + dbTotalVat + ",'" + tenantName + "'," + bill_startINV + "," + bill_endINV + "," + bill_start + "," + bill_end + "," + dbTotalTransaction + "," + dbLocalTax + "," + dbServiceCharge + "," + dbNoTaxSales + "," + dbTotalRawGross + "," + dbLocalTax + "," + dbTotalOthers + "," + TerminalNumber + ")");
            DBFDailyTerminal2(fileName);
            TemplateConnectionClose();
        }

        public void GenerateHourlyDBFFile(string startHour, string fileName)
        {
            TemplateConnection(ayalaFolder + "\\" + strDate.ToString("yyyy"));
            DBFQuery("INSERT INTO " + fileName + "H.DBF Values ('" + dbDateStart + "','" + startHour + "', " + dbhrlySales + ", " + dbhrlyTotalTransaction + ",'" + tenantName + "'," + TerminalNumber + ")");
            TemplateConnectionClose();
        }

        public void GenerateDailyTextFile()
        {
            dailyTextfile.WriteLine(dbDateStart + "," + dbRunningGT + "," + dbNewGT + "," + dbTotalDlySales + "," + dbTotalDiscount + "," + dbTotalREF + "," + dbTotalCAN + "," + dbTotalVat + "," + tenantName + "," + bill_startINV + "," + bill_endINV + "," + bill_start + "," + bill_end + "," + dbTotalTransaction + "," + dbLocalTax + "," + dbServiceCharge + "," + dbNoTaxSales + "," + dbTotalRawGross + "," + dbLocalTax + "," + dbTotalOthers + "," + TerminalNumber);
        }

        public void GenerateHourlyTextFile(string startHour)
        {
            hourlyTextfile.WriteLine(dbDateStart + "," + startHour + "," + dbhrlySales + "," + dbhrlyTotalTransaction + "," + tenantName + "," + TerminalNumber);
        }

        #endregion  

        #region "CopyToAyala"

        public void CopyFileHourlyToAyalaFolder()
        {
            fileNamePath = Directory.GetCurrentDirectory() + "\\Template\\";
            hourlyPath = "hourly.DBF";
            hourlyDBFFilename = tenantCode + strDate.ToString("MMdd") + "H.DBF";
            string sourceFile = Path.Combine(fileNamePath, hourlyPath);
            string destFile = Path.Combine(ayalaFolder + "\\" + strDate.ToString("yyyy"), hourlyDBFFilename);
            File.Copy(sourceFile, destFile, true);
        }

        public void CopyFileDailyToAyalaFolder()
        {
            fileNamePath = Directory.GetCurrentDirectory() + "\\Template\\";
            dailyPath = "daily.DBF";
            dailyDBFFileName = tenantCode + strDate.ToString("MMdd") + ".DBF";
            string sourceFile = Path.Combine(fileNamePath, dailyPath);
            string destFile = Path.Combine(ayalaFolder + "\\" + strDate.ToString("yyyy"), dailyDBFFileName);
            File.Copy(sourceFile, destFile, true);
        }

        #endregion

        #region "Daily Reports"

        public void GenerateDailyReport(string txtPath, string fileName)
        {
            StreamWriter dailyReport = new StreamWriter(txtPath + "\\" + fileName);
            dailyReport.WriteLine("Compuware POS Systems");
            dailyReport.WriteLine("G/F Unit A Cordova Condominium. Valero St.");
            dailyReport.WriteLine("Salcedo Village, Makati City");
            dailyReport.WriteLine();

            dailyReport.WriteLine("TIN: 000-000-000-000");
            dailyReport.WriteLine("Serial No: XX-00000000");
            dailyReport.WriteLine("Machine Ident No: 00000000");
            dailyReport.WriteLine("PTUN: FP000000-000-000000-000000");
            dailyReport.WriteLine("Bir Accredit No.: 000-00000000-000000");
            dailyReport.WriteLine();
            dailyReport.WriteLine();

            dailyReport.WriteLine("CONSOLIDATED REPORT Z-READ");
            dailyReport.WriteLine("----------------------------------------");
            dailyReport.WriteLine("DESCRIPTION                   QTY/AMOUNT");
            dailyReport.WriteLine("----------------------------------------");
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","DAILY SALES",dbTotalDlySales));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","VAT: ", dbTotalVat));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","VATABLE SALES",dbTaxSales));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","NON-VATABLE SALES: ", dbNoTaxSales));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","LESS SC DISC",dbTotalSenior));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","VAT EXEMPT",dbTotalVatExempt));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","ZERO RATED",dbTotalDiplomat));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","NET SALES: " , dbTotalSales));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","TOTAL QTY SOLD",dbTotalNonVoidCount));
            dailyReport.WriteLine(string.Format("{0,-20}  {1,18}","TRANSACTION COUNT: ", dbTotalTransaction));
            dailyReport.WriteLine("----------------------------------------");
            dailyReport.WriteLine(strDate.ToString("d"));
            dailyReport.Close();
        }

        #endregion

        #region "Terminal 2"

        public void DBFDailyTerminal2(string fileName)
        {
            //For Terminal 2
            switch (strTerminal2)
            {
                case 1:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/1/2016',0,500,446.43,0,0,0,53.57,'CPW-COMPUW',1,4,1,4,4,0,44.64,0,554.64,0,5,2)");
                    break;
                case 2:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/2/2016',500,2500,1785.71,0,0,0,214.29,'CPW-COMPUW',5,10,5,10,6,0,178.57,0,2178.57,0,20,2)");
                    break;
                case 3:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/3/2016',2500,2500,0,0,0,0,0,'CPW-COMPUW',10,10,10,10,0,0,0,0,0,0,0,2)");
                    break;
                case 4:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/4/2016',2500,3400,803.57,0,0,0,96.43,'CPW-COMPUW',11,13,11,13,3,0,80.36,0,980.36,0,3,2)");
                    break;
                case 5:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/5/2016',3400,5700,2053.57,0,0,0,246.43,'CPW-COMPUW',14,18,14,18,5,0,205.36,0,2505.36,0,11,2)");
                    break;
                case 6:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/6/2016',5700,7500,1607.14,0,0,0,192.86,'CPW-COMPUW',19,22,19,22,4,0,160.71,0,1960.71,0,6,2)");
                    break;
                case 7:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/7/2016',7500,7500,0,0,0,0,0,'CPW-COMPUW',22,22,22,22,0,0,0,0,0,0,0,2)");
                    break;
                case 8:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/8/2016',7500,9200,1517.86,0,0,0,182.14,'CPW-COMPUW',23,29,23,29,7,0,151.79,0,1851.79,0,9,2)");
                    break;
                case 9:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/9/2016',9200,9200,0,0,0,0,0,'CPW-COMPUW',29,29,29,29,0,0,0,0,0,0,0,2)");
                    break;
                case 10:
                    DBFQuery("INSERT INTO " + fileName + ".DBF VALUES ('2/10/2016',9200,10400,1071.43,0,0,0,128.57,'CPW-COMPUW',30,33,30,33,4,0,107.14,0,1307.14,0,4,2)");
                    break;
                default:
                    break;
            }
        }

        public void TXTDailyTerminal2()
        {
            switch (strTerminal2)
            {
                case 1:
                    dailyTextfile.WriteLine("2/1/2016,0,500,446.43,0,0,0,53.57,CPW-COMPUW,1,4,1,4,4,0,44.64,0,554.64,0,5,2");
                    break;
                case 2:
                    dailyTextfile.WriteLine("2/2/2016,500,2500,1785.71,0,0,0,214.29,CPW-COMPUW,5,10,5,10,6,0,178.57,0,2178.57,0,20,2");
                    break;
                case 3:
                    dailyTextfile.WriteLine("2/3/2016,2500,2500,0,0,0,0,0,CPW-COMPUW,10,10,10,10,0,0,0,0,0,0,0,2");
                    break;
                case 4:
                    dailyTextfile.WriteLine("2/4/2016,2500,3400,803.57,0,0,0,96.43,CPW-COMPUW,11,13,11,13,3,0,80.36,0,980.36,0,3,2");
                    break;
                case 5:
                    dailyTextfile.WriteLine("2/5/2016,3400,5700,2053.57,0,0,0,246.43,CPW-COMPUW,14,18,14,18,5,0,205.36,0,2505.36,0,11,2");
                    break;
                case 6:
                    dailyTextfile.WriteLine("2/6/2016,5700,7500,1607.14,0,0,0,192.86,CPW-COMPUW,19,22,19,22,4,0,160.71,0,1960.71,0,6,2");
                    break;
                case 7:
                    dailyTextfile.WriteLine("2/7/2016,7500,7500,0,0,0,0,0,CPW-COMPUW,22,22,22,22,0,0,0,0,0,0,0,2");
                    break;
                case 8:
                    dailyTextfile.WriteLine("2/8/2016,7500,9200,1517.86,0,0,0,182.14,CPW-COMPUW,23,29,23,29,7,0,151.79,0,1851.79,0,9,2");
                    break;
                case 9:
                    dailyTextfile.WriteLine("2/9/2016,9200,9200,0,0,0,0,0,CPW-COMPUW,29,29,29,29,0,0,0,0,0,0,0,2");
                    
                    break;
                case 10:
                    dailyTextfile.WriteLine("2/10/2016,9200,10400,1071.43,0,0,0,128.57,CPW-COMPUW,30,33,30,33,4,0,107.14,0,1307.14,0,4,2");
                    
                    break;
                default:
                    break;
            }
        }

        public void DBFHourlyTerminal2(string fileName)
        {
            TemplateConnection(ayalaFolder + "\\" + strDate.ToString("yyyy"));
            switch (strTerminal2)
            {
                case 1:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','9:00',250,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','11:00',250,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/1/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 2:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','9:00',250,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','11:00',250,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','13:00',250,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','14:00',250,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','20:00',500,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','21:00',500,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/2/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 3:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','9:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','11:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/3/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 4:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','9:00',200,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','11:00',200,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','16:00',500,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/4/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 5:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','9:00',460,4,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','11:00',460,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','13:00',460,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','15:00',460,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','17:00',460,3,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/5/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 6:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','9:00',700,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','11:00',300,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','15:00',550,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','19:00',250,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/6/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 7:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','9:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','11:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/7/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 8:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','9:00',300,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','11:00',400,2,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','14:00',250,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','16:00',150,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','19:00',200,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','21:00',100,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','23:00',300,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/8/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 9:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','8:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','9:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','11:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','14:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','17:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/9/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;

                case 10:
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','6:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','7:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','8:00',0,0,'CPW-COMPUW',2)");;
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','9:00',300,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','10:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','11:00',300,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','12:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','13:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','14:00',300,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','15:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','16:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','17:00',300,1,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','18:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','19:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','20:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','21:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','22:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','23:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','0:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','1:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','2:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','3:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','4:00',0,0,'CPW-COMPUW',2)");
                    DBFQuery("INSERT INTO " + fileName + "H.DBF Values('2/10/2016','5:00',0,0,'CPW-COMPUW',2)");
                    break;
                default:
                    break;
            }
            TemplateConnectionClose();
        }

        public void TXTHourlyTerminal2()
        {
            switch (strTerminal2)
            {
                case 1:
                    hourlyTextfile.WriteLine("2/1/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,9:00,250,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,11:00,250,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/1/2016,5:00,0,0,CPW-COMPUW,2");
                    break;

                case 2:
                    hourlyTextfile.WriteLine("2/2/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,9:00,250,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,11:00,250,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,13:00,250,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,14:00,250,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,20:00,500,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,21:00,500,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/2/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 3:
                    hourlyTextfile.WriteLine("2/3/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,9:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,11:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/3/2016,5:00,0,0,CPW-COMPUW,2");
                    break;

                case 4:
                    hourlyTextfile.WriteLine("2/4/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,9:00,200,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,11:00,200,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,16:00,500,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/4/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 5:
                    hourlyTextfile.WriteLine("2/5/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,9:00,460,4,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,11:00,460,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,13:00,460,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,15:00,460,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,17:00,460,3,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/5/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 6:
                    hourlyTextfile.WriteLine("2/6/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,9:00,700,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,11:00,300,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,15:00,550,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,19:00,250,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/6/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 7:
                    hourlyTextfile.WriteLine("2/7/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,9:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,11:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/7/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 8:
                    hourlyTextfile.WriteLine("2/8/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,9:00,300,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,11:00,400,2,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,14:00,250,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,16:00,150,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,19:00,200,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,21:00,100,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,23:00,300,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/8/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 9:
                    hourlyTextfile.WriteLine("2/9/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,9:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,11:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,14:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,17:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/9/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                case 10:
                    hourlyTextfile.WriteLine("2/10/2016,6:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,7:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,8:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,9:00,300,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,10:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,11:00,300,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,12:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,13:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,14:00,300,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,15:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,16:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,17:00,300,1,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,18:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,19:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,20:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,21:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,22:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,23:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,0:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,1:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,2:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,3:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,4:00,0,0,CPW-COMPUW,2");
                    hourlyTextfile.WriteLine("2/10/2016,5:00,0,0,CPW-COMPUW,2");
                    break;
                default:
                    break;
            }
        }

        public void Terminal2Reports()
        {
            switch (strTerminal2)
            {
                case 1:
                    dbTotalVat += 53.57;
                    dbTotalDlySales += 446.43;
                    dbTaxSales += 446.43;
                    dbTotalSales += 446.43;
                    dbTotalNonVoidCount += 4;
                    dbTotalTransaction += 4;
                    break;

                case 2:
                    dbTotalVat += 214.29;
                    dbTotalDlySales += 1785.71;
                    dbTaxSales += 1785.71;
                    dbTotalSales += 1785.71;
                    dbTotalNonVoidCount += 6;
                    dbTotalTransaction += 6;
                    break;

                case 4:
                    dbTotalVat += 96.43;
                    dbTotalDlySales += 803.57;
                    dbTaxSales += 803.57;
                    dbTotalSales += 803.57;
                    dbTotalNonVoidCount += 3;
                    dbTotalTransaction += 3;
                    break;

                case 5:
                    dbTotalVat += 246.43;
                    dbTotalDlySales += 2053.57;
                    dbTaxSales += 2053.57;
                    dbTotalSales += 803.57;
                    dbTotalNonVoidCount += 5;
                    dbTotalTransaction += 5;
                    break;

                case 6:
                    dbTotalVat += 192.86;
                    dbTotalDlySales += 1607.14;
                    dbTaxSales += 1607.14;
                    dbTotalSales += 1607.14;
                    dbTotalNonVoidCount += 4;
                    dbTotalTransaction += 4;
                    break;

                case 8:
                    dbTotalVat += 182.14;
                    dbTotalDlySales += 1517.86;
                    dbTaxSales += 1517.86;
                    dbTotalSales += 1517.86;
                    dbTotalNonVoidCount += 7;
                    dbTotalTransaction += 7;
                    break;

                case 10:
                    dbTotalVat += 128.57;
                    dbTotalDlySales += 1071.43;
                    dbTaxSales += 1071.43;
                    dbTotalSales += 1071.43;
                    dbTotalNonVoidCount += 4;
                    dbTotalTransaction += 4;
                    break;
                default:
                    dbTotalVat += 0;
                    dbTotalDlySales += 0;
                    dbTaxSales += 0;
                    dbTotalSales += 0;
                    dbTotalNonVoidCount += 0;
                    dbTotalTransaction += 0;
                    break;
            }
        }

        #endregion
    }
}
