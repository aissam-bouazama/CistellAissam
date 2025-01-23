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
            // Crear o recuperar el cistell de la sesión
            var cistell = HttpContext.Session.GetObject<List<CistellItem>>("Cistell") ?? new List<CistellItem>();

            // Verificar si el producto ya está en el cistell
            var itemExistente = cistell.FirstOrDefault(x => x.Id == id);
            if (itemExistente != null)
            {
                // Si ya existe, actualizamos la cantidad
                itemExistente.Quantitat += quantitat;
            }
            else
            {
                // Si no existe, añadimos un nuevo elemento
                cistell.Add(new CistellItem
                {
                    Id = id,
                    Nom = nom,
                    Quantitat = quantitat,
                    Preu = preu
                });
            }

            // Guardar el cistell actualizado en la sesión
            HttpContext.Session.SetObject("Cistell", cistell);

            // Redirigir a la vista del cistell o retornar una respuesta
            return View("Cistell", cistell); // Pasar el cistell a la vista
        }

            return null;
        }
    }
}
