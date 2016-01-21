using System.Data;
using System.Data.OleDb;
using System.IO;
using System;
namespace Ayala_Interface_dotNet.ClassCon
{
    public class classWriteFile : classReprocess
    {
        #region "Fields"
        private OleDbDataAdapter oleFile;

        #endregion

        public classWriteFile()
        {
            TemplateConnection();
        }

        public void CopyTemplate()
        {
            //File.Copy(Environment.CurrentDirectory + "\Template\DAY.DBF","C:\Ayala\2015\NewDay.DBF");
        }
        public void GenerateFile()
        {
            //DBFQuery("Insert Into DAY (trancnt) values (" + dbTotalTransaction + " )");           
        }
    }
}
