using CistellAissam.Models;

namespace CistellAissam.Repository.Interfaces
{
    public interface IUsuariRepository
    {
        bool BloquejarUsuari(Usuari usuari);
        Usuari? getUsuari(string email);
    }
}