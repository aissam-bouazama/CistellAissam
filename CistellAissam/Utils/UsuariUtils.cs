using CistellAissam.Data;
using CistellAissam.Models;
using CistellAissam.Repository;
using System.Text.Json;

namespace CistellAissam.Utils
{
    public class UsuariUtils
    {
        public static bool BloquejarUsuari(UsuariLogin usuari,TiendaContext _DBContext)
        {
            //var usuaris = Usuaris._usuaris;
            //int index = usuaris.IndexOf(usuari);
            var user = _DBContext.usuariLogins.Single(b => b.email == usuari.email);
            if(user != null)
            {
                user.locked = true;
                user.lastupdate = DateTime.Now;
                _DBContext.SaveChanges();
                return true;
            }
         
           /* if (index != -1)
            {
                Usuaris._usuaris[index].locked = true;
                Usuaris._usuaris[index].lastupdate = DateTime.Now;
                return true;
            }*/
            return false;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuari"></param>
        /// <param name="count"></param>
        /// <returns>devuelve el numero de intentos en el caso q passa los intentos permitidos Bloquejara el Usuari</returns>
        public static int ControlintentosLogin(UsuariLogin usuari, int count,TiendaContext _DBContext)
        {
            UsuariRepository usuaris = new();
            if (count == 2)
            {
               // usuaris.BloquejarUsuari(usuari);
               UsuariUtils.BloquejarUsuari(usuari, _DBContext);
                return count;

            }
            else if (count > 0)
            {
                count += 1;
            }
            else
            {
                count = 1;
            }
            return count;
        }

        public static bool IsadminUsuari(HttpContext httpContext)
        {
            var user = httpContext.Session.GetString("usuarisession");
            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<UsuariLogin>(user);
                if (userauth.isAdmin)
                {
                    return true;
                }


            }
            return false;

        }

    }
}
