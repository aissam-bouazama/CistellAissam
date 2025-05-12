using CistellAissam.Data;
using CistellAissam.Models;
using CistellAissam.Repository.Interfaces;
using CistellAissam.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CistellAissam.Controllers
{
    public class ProducteController : Controller
    {

        /*  ProducteRepository repo = new();*/
        private readonly IProducteRepository _RepoProducte;
        private readonly TiendaContext _DBContext;

        public ProducteController(IProducteRepository repo,TiendaContext _dbcontext)
        {
            this._DBContext = _dbcontext;
            this._RepoProducte = repo;
        }


        public IActionResult Index()
        {

            ViewData["userauth"] = SessionUtils.ObtenerUsuariAuth(HttpContext);

            if (UsuariUtils.IsadminUsuari(HttpContext))
            {
                ViewData["pagina"] = "afegir";
                return View("AfegirProducte");
            }
            else
            {
                return LocalRedirect("/");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="producte"></param>
        /// <param name="imatgeproducte"></param>
        /// <returns>Accés només al administrador (Afegir Produe Nou)</returns>
        public async Task<IActionResult> AfegirProducte(Producte producte, IFormFile imatgeproducte)
        {
       
            if (UsuariUtils.IsadminUsuari(HttpContext))
            {

                ViewData["pagina"] = "afegir";
                ModelState.Clear();

                var imatge = await CargarImatge(imatgeproducte);
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
                    }
                    else if (ProducteUtils.Add(producte,_DBContext))
                    {
                        ViewData["resultat"] = "Producte Afegit Correctament";
                    }
                    else
                    {
                        ModelState.AddModelError("codiProducte", "Aquest Codi de Producte ja existeix ");
                        return View("AfegirProducte", producte);
                    }

                }
            }

            return LocalRedirect("/");
        }
        /// <summary>
        /// Cargar la imatge al Projecte 
        /// </summary>
        /// <param name="file">Imatge que vols cargar al Projecte </param>
        /// <returns>el nom de la imagen guardada</returns>
        public async Task<string> CargarImatge(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            string[] permittedExtensions = { ".png", ".jpg", ".jpeg" };

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
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
