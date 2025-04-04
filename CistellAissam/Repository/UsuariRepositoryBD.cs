using CistellAissam.Models;
using CistellAissam.Repository.Interfaces;
using MySql.Data.MySqlClient;
using MySql.Data;
using CistellAissam.Data;

namespace CistellAissam.Repository
{
    public class UsuariRepositoryBD : IUsuariRepository
    {
		MySqlConnection? _conection;
		MySqlTransaction trans;
		public UsuariRepositoryBD(MySqlConnection connection, MySqlTransaction trans)
		{
			this._conection = connection;
            this.trans = trans;
		}
		public bool BloquejarUsuari(Usuari usuari)
        {
            throw new NotImplementedException();
        }
		
		public bool Check(string email,string password)
        {
            MySqlConnection? conn = DB.Open();
            if (conn == null) return false;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT count(*) from users WHERE email = @useremail" +
                "AND password = @password";
            cmd.Parameters.AddWithValue("@useremail",email);
            cmd.Parameters.AddWithValue("@password",password);
            long num = (long) cmd.ExecuteScalar();// devuelve solo un registro utilizarem el scalar 
            conn.Close();
           return num > 0;
        }

        public void Add(Usuari usuari) { 

        }

        public Usuari? getUsuari(string email)
        {
            Usuari? usuari = null;
            string query = "SELECT * from users where email = @useremail";
            MySqlConnection? conn = DB.Open();
            if (conn == null) return usuari;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@useremail", email);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) { 
                usuari = new Usuari();
                usuari.email = email;
                usuari.password = reader.GetString("password");
                usuari.isAdmin = reader.GetBoolean("isadmin");
                usuari.locked = reader.GetBoolean("locked");
                if (!reader.IsDBNull(reader.GetOrdinal("lastupdated")))
                {
                    usuari.lastupdate = reader.GetDateTime("lastupdated");
                }
            }
            conn.Close();
            return usuari;
        }
    }
}
