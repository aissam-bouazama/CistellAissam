using CistellAissam.Repository.Interfaces;
using CistellAissam.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CistellAissam.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuariRepository _usuariRepository;

        public LoginController(IUsuariRepository usuariRepository)
        {
            _usuariRepository = usuariRepository;
        }

        public IActionResult Index()
        {
            return View("login");
        }
        [HttpPost]
        public IActionResult Login()
        {
            string email = Request.Form["email"];
            string Contrasenya = Request.Form["password"];
            ModelState.Clear();
            var usuari = _usuariRepository.getUsuari(email);
            if (usuari != null)
            {
                int count = 0;
                if (HttpContext.Session.GetInt32(email) != null)
                {
                    count = (int)HttpContext.Session.GetInt32(email);
                }
                if (!string.IsNullOrEmpty(Contrasenya))
                {

                    if (usuari.password.Equals(Contrasenya))
                    {
                        if (!usuari.locked)
                        {
                            SessionUtils.AfegirUsuariSessio(HttpContext, usuari);
                            return LocalRedirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError("email", "éstas Bloquejat Contacta amb el administrador");
                            return View("login", usuari);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("password", "La Contrasenya no és Correcte");
                        if (!usuari.locked)
                        {
                            count = UsuariUtils.ControlintentosLogin(usuari, count);
                        }
                        HttpContext.Session.SetInt32(email, count);
                        return View("login", usuari);
                    }


                }
                else
                {
                    ModelState.AddModelError("password", "el Camp de  Contrasenya no pot estar en blanc");
                    return View("login", usuari);
                }
            }
            else
            {
                ModelState.AddModelError("email", "Aquest email no existeix ");
                return View("login", usuari);
            }


            return LocalRedirect("/");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("usuarisession");
            return LocalRedirect("/");

        }

    }
}
