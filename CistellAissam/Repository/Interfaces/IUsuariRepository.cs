using CistellAissam.Models;
using MySql.Data.MySqlClient;

namespace CistellAissam.Repository.Interfaces
{
    
	public interface IUsuariRepository
    {
        bool BloquejarUsuari(UsuariLogin usuari);
        UsuariLogin? getUsuari(string email);
    }
}