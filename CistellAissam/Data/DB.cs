using MySql.Data.MySqlClient;

namespace CistellAissam.Data
{
    public static class DB
    {
        static public MySqlConnection? Open()
        {
             MySqlConnection? conn = new MySqlConnection();
            conn.ConnectionString = "server=127.0.0.1;uid=root;pwd=user;database=tenda";
            conn.Open();
            return conn;
        }
    }
}
