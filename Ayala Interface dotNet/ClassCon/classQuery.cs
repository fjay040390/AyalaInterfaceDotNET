
namespace Ayala_Interface_dotNet.ClassCon
{
    public class classQuery : classDBConnection
    {
        public string userPass { get; set; }
        public string adminPass { get; set; }

        public classQuery()
        {
            
        }

        public void GetLoginDetails()
        {
            //SELECT query for login//fire query
            Queries("Select userPassword,adminPassword FROM tblLogin");
            //rdr.Read();
            if (rdr.HasRows)
            {
                //read data from database
                while (rdr.Read())
                {
                    userPass = rdr.GetValue(0).ToString();
                    adminPass = rdr.GetValue(1).ToString();
                }
            }
        }
    }
}
