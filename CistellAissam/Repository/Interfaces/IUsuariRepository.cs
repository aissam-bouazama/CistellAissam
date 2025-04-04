using CistellAissam.Models;
using MySql.Data.MySqlClient;

namespace CistellAissam.Repository.Interfaces
{
    
	public interface IUsuariRepository
    {
        bool BloquejarUsuari(Usuari usuari);
        Usuari? getUsuari(string email);
    }
}