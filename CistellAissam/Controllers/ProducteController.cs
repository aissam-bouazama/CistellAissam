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
        public async Task<IActionResult> AfegirProducte(Producte producte,IFormFile imatgeproducte)
        {
            ViewData["pagina"] = "afegir";

             var  imatge = OnPostUploadAsync(imatgeproducte);
            if (string.IsNullOrEmpty(imatge))
            {
                ViewData["resultat"] = "No s'ha pogut pujar la imatge del producte....";
                return View("AfegirProducte", producte);
            }
            else
            {
                 producte.imatgeproducte =  imatge;
                Console.WriteLine(producte.imatgeproducte);
                Console.WriteLine(producte.codiProducte);
                Console.WriteLine(producte.preuProducte);


                if (!TryValidateModel(producte))
                {
                    return View("AfegirProducte", producte);
                }
                if (repo.AfegirProducte(producte))
                {
                    ViewData["resultat"] = "Producte Afegit Correctament";
                }
                else
                {
                    ViewData["resultat"] = "Producte Aquest codi del producte ja existeix ..";

                }
            }


            return View("AfegirProducte");
        }
        public  string OnPostUploadAsync(IFormFile files)
        {

            var uniqueFileName = string.Empty;
            if (files != null && files.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imatgesProductes");
                if (!Directory.Exists(uploadsFolder))
                {
                     Directory.CreateDirectory(uploadsFolder);
                    return null;
                }

                 uniqueFileName = $"{Guid.NewGuid()}_{files.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                     files.CopyTo(stream);
                }

               
            }
            return uniqueFileName;
        }

    }
}
