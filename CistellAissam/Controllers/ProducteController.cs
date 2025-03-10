﻿using Microsoft.AspNetCore.Mvc;
using CistellAissam.Models;
using CistellAissam.Data;
using static System.Net.Mime.MediaTypeNames;
using CistellAissam.Repository;

namespace CistellAissam.Controllers
{
    public class ProducteController : Controller
    {
        CistellRepository repo = new();
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
                    return View("AfegirProducte", producte);
                }
               
            }
            return LocalRedirect("/");
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

             string[] permittedExtensions = { ".png", ".jpg",".jpeg" };

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
