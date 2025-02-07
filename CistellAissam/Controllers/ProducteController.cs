using Microsoft.AspNetCore.Mvc;

namespace CistellAissam.Controllers
{
    public class ProducteController : Controller
    {
        public IActionResult Index()
        {
            return View("AfegirProducte");
        }
    }
}
