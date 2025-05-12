using CistellAissam.Data;
using CistellAissam.Models;
using CistellAissam.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CistellAissam.Controllers
{
    public class UsuariController : Controller
    {
        private readonly TiendaContext _DBContext;
        public UsuariController(TiendaContext context)
        {
            this._DBContext = context;
        }
        public IActionResult Index()
        {
            var userauth = SessionUtils.ObtenerUsuariAuth(HttpContext);
            ViewData["userauth"] = userauth;
            if(UsuariUtils.IsadminUsuari(HttpContext))
            {
                var usuaris = _DBContext.usuaris.ToList();
                return View("Index", usuaris);
            }
            else
            {
                return LocalRedirect("/");
            }
           
        }
       public IActionResult Details(string id)
        {
            if(UsuariUtils.IsadminUsuari(HttpContext))
            {
                var usuari = _DBContext.usuaris.FirstOrDefault(x => x.Email == id);
                return View("Details", usuari);
            }
            else
            {
                return LocalRedirect("/");
            }
           
        }
        public IActionResult Edit(string id)
        {
            if(UsuariUtils.IsadminUsuari(HttpContext))
            {
                var usuari = _DBContext.usuaris.FirstOrDefault(x => x.Email == id);
                return View("Edit", usuari);
            }
            else
            {
                return LocalRedirect("/");
            }
           
        }
        [HttpPost]
        public IActionResult Edit(Usuari usuari)
        {
            if(UsuariUtils.IsadminUsuari(HttpContext))
            {
                if (!ModelState.IsValid)
                {
                    RedirectToAction("Edit", usuari);
                }
                var usuariBD = _DBContext.usuaris.FirstOrDefault(x => x.Email == usuari.Email);
                if (usuariBD != null)
                {
                    usuariBD.Nom = usuari.Nom;
                    usuariBD.Cognom = usuari.Cognom;
                    usuariBD.Telefon = usuari.Telefon;
                    usuariBD.DataNaixement = usuari.DataNaixement;
                    _DBContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return LocalRedirect("/");
            }
           
        }


    }
}
