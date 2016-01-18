using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Ayala_Interface_dotNet.ClassCon
{
    public class classReprocess : classQuery
    {
        #region "FilterDate"

        public void FilterDate()
        {
            endDate = Convert.ToDateTime(dateEnd);
            strDate = Convert.ToDateTime(dateStart);

            do
            {
                repYear = strDate.ToString("yy");
                GetSessionNo();
                SaveFilterDate(sessNo);
                strDate = strDate.AddDays(1);
            } while (strDate <= endDate);
        }

        #endregion

        public void GetSessionNo()
        {
           LoadRmPath();
          openRMConnection();
           cmd = new OleDbCommand("SELECT session_no FROM REP" + repYear + ".DBF WHERE date_start = #" + strDate + "#",con);
           cmd.ExecuteReader();
            while (rdr.Read()) {
                sessNo = rdr.GetValue(0).ToString();
            }
        }

        public void SaveFilterDate(string str_date)
        {
            Queries("INSERT INTO tblFilterDate VALUES (" + str_date + ")");
        }

        public void LoadRmPath()
        {
            openConnection();
            cmd = new OleDbCommand("Select RMPath FROM tblLogin",con);
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    rmPath = rdr.GetValue(0).ToString();
                }
            }
        }
         
    }
}
