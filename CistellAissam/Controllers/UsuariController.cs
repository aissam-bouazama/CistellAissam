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
            ViewData["userauth"] = SessionUtils.ObtenerUsuariAuth(HttpContext);
            var usuaris = _DBContext.usuaris.ToList();
            return View("Index", usuaris);
        }
       public IActionResult Details(string id)
        {
            var usuari = _DBContext.usuaris.FirstOrDefault(x => x.Email == id);
            return View("Details", usuari);
        }
        public IActionResult Edit(string id)
        {
            var usuari = _DBContext.usuaris.FirstOrDefault(x => x.Email == id);
            return View("Edit", usuari);
        }
        [HttpPost]
        public IActionResult Edit(Usuari usuari)
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


    }
}
