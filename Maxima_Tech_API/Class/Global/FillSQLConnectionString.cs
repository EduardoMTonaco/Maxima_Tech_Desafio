using Maxima_Tech_API.Class.DBHandler;

namespace Maxima_Tech_API.Class.Global
{
    public class FillSQLConnectionString
    {
        public FillSQLConnectionString(string connectionString) 
        {
            MySQLHandler mySQLHandler = new MySQLHandler(connectionString);
        }
       
    }
}
