using CistellAissam.Models; 
using CistellAissam.Data;
namespace CistellAissam.Repository
{
    public class UsuariRepository
    {
        public Usuari? getUsuari(string email)
        {
            foreach (Usuari us in Usuaris._usuaris)
            {
                if (us.email == email)
                {
                    return us;
                }
            }
            return null;
        }

        public bool BloquejarUsuari(Usuari usuari)
        {
            var usuaris = Usuaris._usuaris;
           int index = usuaris.IndexOf(usuari);
            if(index != -1)
            {
                Usuaris._usuaris[index].locked = true;
                Usuaris._usuaris[index].lastupdate = DateTime.Now;
                return true;
            }
            return false;
            
        }

    }
}
