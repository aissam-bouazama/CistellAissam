using CistellAissam.Models;
using CistellAissam.Repository;
using System.Text.Json;

namespace CistellAissam.Utils
{
    public class UsuariUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuari"></param>
        /// <param name="count"></param>
        /// <returns>devuelve el numero de intentos en el caso q passa los intentos permitidos Bloquejara el Usuari</returns>
        public static int ControlintentosLogin(Usuari usuari, int count)
        {
            UsuariRepository usuaris = new();
            if (count == 2)
            {
                usuaris.BloquejarUsuari(usuari);
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
                var userauth = JsonSerializer.Deserialize<Usuari>(user);
                if (userauth.isAdmin)
                {
                    return true;
                }


            }
            return false;

        }

    }
}
