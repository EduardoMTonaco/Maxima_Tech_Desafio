using MySql.Data.MySqlClient;

namespace Maxima_Tech_API.Class.DBHandler
{
    public class MySQLHandler
    {
            public MySQLHandler(string _connectionString)
            {
                connectionString = _connectionString;
            }
            private static string connectionString { get; set; } = "";
            public static void SQLCommand(string sql)
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            public static List<Array> SQLReader(string sql)
            {
                try
                {
                    List<Array> ArrayList = new List<Array>();
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        List<string> list = new List<string>();
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        connection.Open();
                       
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                object[] retorno = new object[rdr.FieldCount];
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {

                                    retorno.SetValue(rdr.GetValue(i), i);
                                }
                                ArrayList.Add(retorno);
                            }
                        }
                        connection.Close();
                    }
                    return ArrayList;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
