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
            return View("AfegirProducte");
        }
        public IActionResult AfegirProducte(Producte producte)
        {
            Console.WriteLine("AfegirProducte");
            Console.WriteLine(producte.imatgeproducte);
            if (!ModelState.IsValid)
            {
                return View("AfegirProducte");
            }

           
            repo.AfegirProducte(producte);

            return RedirectToAction("Index");
        }
    }
}
