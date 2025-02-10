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
                return View("AfegirProducte");
            }

           
            repo.AfegirProducte(producte);

            return RedirectToAction("Index");
        }
    }
}
