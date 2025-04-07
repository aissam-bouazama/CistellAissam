using CistellAissam.Data;
using CistellAissam.Models;
using CistellAissam.Repository.Interfaces;
namespace CistellAissam.Repository
{
    public class UsuariRepository : IUsuariRepository
    {
        public UsuariLogin? getUsuari(string email)
        {
            foreach (UsuariLogin us in Usuaris._usuaris)
            {
                if (us.email == email)
                {
                    return us;
                }
            }
            return null;
        }

        public bool BloquejarUsuari(UsuariLogin usuari)
        {
            var usuaris = Usuaris._usuaris;
            int index = usuaris.IndexOf(usuari);
            if (index != -1)
            {
                Usuaris._usuaris[index].locked = true;
                Usuaris._usuaris[index].lastupdate = DateTime.Now;
                return true;
            }
            return false;

        }

    }
}
