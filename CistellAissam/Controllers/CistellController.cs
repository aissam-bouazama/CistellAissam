using CistellAissam.Data;
using CistellAissam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace CistellAissam.Controllers
{
    public class CistellController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            CistellRepo repo = new CistellRepo();
          var productos= repo.ObtenirProductos();
            int numproductescistell = 0;
            if (HttpContext.Session.GetInt32("Contador") != null)
            {
                numproductescistell = (int)HttpContext.Session.GetInt32("Contador");
            }


            ViewData["contador"] = numproductescistell;

            return View("Cistell", productos);
        }
        [HttpPost]
        public IActionResult Afegir()
        {

            
            var codeproducte = Request.Form["codeproducte"];
            
            string[] cistell = { codeproducte, "1" };
            int quantitatcista = 1;
            int counproducts = 1;
            string jsonserialize = JsonSerializer.Serialize(cistell);
            if(HttpContext.Session.GetString(codeproducte)!=null){
                string producte = (string)HttpContext.Session.GetString(codeproducte);
                string[] productecistell = JsonSerializer.Deserialize<string[]>(producte);
                
                counproducts = int.Parse(productecistell[1])+1;
                string[] cistellambquantitat =  { codeproducte, counproducts.ToString() };
                HttpContext.Session.SetString(codeproducte, JsonSerializer.Serialize(cistellambquantitat));
            }
            else
            {
               
                HttpContext.Session.SetString(codeproducte, jsonserialize);

            }
            if (HttpContext.Session.GetInt32("Contador") != null)
            {
                quantitatcista = (int)HttpContext.Session.GetInt32("Contador") + 1;
            }





            HttpContext.Session.SetInt32("Contador", quantitatcista);
            
            return RedirectToAction("Index");
        }
        public IActionResult Cestill()
        {
            //incializar el repositorio para obtener los productos
            CistellRepo repository = new CistellRepo();
            var productos = repository.ObtenirProductos();
            //inicializar la lista de productos para almacenar los productos que se encuentran en la session
            List<Producte> pr = new();
            //inicializar el variable para almacenar el precio total de productos del cistell
            var preuTotal = 0.0;
            //quantita del producte 
            int quantitat = 0;
            foreach (var producte in productos)
            {
                var prosession = HttpContext.Session.GetString(producte.codiProducte);
                if (prosession != null)
                {

                    
                    var productecistell = JsonSerializer.Deserialize<string[]>(prosession);
                     quantitat = int.Parse(productecistell[1]);
                    for(int i = 0; i < quantitat; i++)
                    {
                        preuTotal += producte.preuProducte;
                    }
                    //passar la quantitat por el view data 
                    ViewData["quantitat"+producte.codiProducte] = quantitat;
                    ViewData["preuTotalt" + producte.codiProducte] = quantitat * producte.preuProducte;

                    pr.Add(new Producte
                    {
                        codiProducte =producte.codiProducte,
                        imatgeproducte = producte.imatgeproducte,
                        nomProducte = producte.nomProducte,
                        preuProducte = producte.preuProducte,
                    });
                }
            }
            //passar a la vista el precio total de productos del cistell
            ViewData["preuTotal"] = preuTotal;
            //passar la quantitat dels productes del cistell
            int numproductescistell = 0;
            if (HttpContext.Session.GetInt32("Contador") != null)
            {
                numproductescistell = (int)HttpContext.Session.GetInt32("Contador");
            }


            ViewData["contador"] = numproductescistell;


            return View("cistellCompra", pr);
        }
        public IActionResult ActualizarQuantitatCistell()
        {
            var Cistellrepo = new CistellRepo();
            string codeproducte = Request.Form["codeproducte"];
            int novaquantitat = int.Parse(Request.Form["quantitat"]);
            Cistellrepo.getProducte(codeproducte);
            if(novaquantitat != 0)
            {
                var producteSession=JsonSerializer.Deserialize<string[]>(HttpContext.Session.GetString(codeproducte));
                producteSession[1] = novaquantitat.ToString();
                string[] cistell = { codeproducte, producteSession[1]  };
                HttpContext.Session.SetString(codeproducte, JsonSerializer.Serialize(cistell));
                int numproductescistell = (int)HttpContext.Session.GetInt32("Contador");
                if (numproductescistell != null)
                {
                    HttpContext.Session.SetInt32("contador",numproductescistell-novaquantitat);

                }
            }

            return RedirectToAction("Cestill");
        }
    }
}
