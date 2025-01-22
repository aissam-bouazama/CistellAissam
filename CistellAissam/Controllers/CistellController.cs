using CistellAissam.Data;
using CistellAissam.Models;
using Microsoft.AspNetCore.Mvc;

namespace CistellAissam.Controllers
{
    public class CistellController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            CistellRepo repo = new CistellRepo();
          var productos= repo.ObtenirProductos();
            
            return View("Cistell", productos);
        }
        [HttpPost]
        public IActionResult Afegir()
        {
            /* if (!ModelState.IsValid)
             {
                 return View("Cistell");
             }
             else
             {
                 return Content("registrat de moment al cistell  falta code");
             }*/

            return null;
        }
    }
}
