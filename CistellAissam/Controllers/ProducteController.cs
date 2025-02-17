using Microsoft.AspNetCore.Mvc;
using CistellAissam.Models;
using CistellAissam.Data;

namespace CistellAissam.Controllers
{
    public class ProducteController : Controller
    {
        CistellRepo repo = new CistellRepo();
        public IActionResult Index()
        {
            ViewData["pagina"] = "afegir";
            return View("AfegirProducte");
        }
        public async Task<IActionResult> AfegirProducte(Producte producte)
        {
           
            if (!ModelState.IsValid)
            {
                ViewData["pagina"] = "afegir";
                return View("AfegirProducte",producte);
            }
            if (repo.AfegirProducte(producte))
            {
                ViewData["resultat"] = "Producte Afegit Correctament";
            }else
            {
                ViewData["resultat"] = "Producte Aquest codi del producte ja existeix ..";
            }

            return RedirectToAction("Index");
        }
    }
}
