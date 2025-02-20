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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="producte"></param>
        /// <param name="imatgeproducte"></param>
        /// <returns></returns>
        public async Task<IActionResult> AfegirProducte(Producte producte,IFormFile imatgeproducte)
        {
            ViewData["pagina"] = "afegir";
            ModelState.Clear();
            if (string.IsNullOrEmpty(producte.codiProducte))
            {
                ModelState.AddModelError("codiProducte", "El codi del producte és obligatori.");
            }

            if (string.IsNullOrEmpty(producte.nomProducte))
            {
                ModelState.AddModelError("nomProducte", "El nom del producte és obligatori.");
            }

            if (producte.preuProducte <= 0)
            {
                ModelState.AddModelError("preuProducte", "El preu del producte ha de ser més gran que 0.");
            }

            if (imatgeproducte == null || imatgeproducte.Length == 0)
            {
                ModelState.AddModelError("imatgeproducte", "L'imatge del producte és obligatòria.");
            }

            var  imatge = await OnPostUploadAsync(imatgeproducte);
            if (string.IsNullOrEmpty(imatge))
            {
                ModelState.AddModelError("imatgeproducte", "No s'ha pogut pujar la imatge del producte....");
                return View("AfegirProducte", producte);
            }
            else
            {
                producte.imatgeproducte = imatge;

                
                if (!TryValidateModel(producte))
                {
                    return View("AfegirProducte", producte);
                } else if (repo.AfegirProducte(producte)){
                    ViewData["resultat"] = "Producte Afegit Correctament";
                }
                else
                {
                    ModelState.AddModelError("codiProducte", "Aquest Codi de Producte ja existeix ");
                }
               
            }


            return View("AfegirProducte");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> OnPostUploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;  
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imatgesProductes/Productes");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); 
            }

            return uniqueFileName;
        }



    }
}
