using CistellAissam.Data;
using CistellAissam.Models;
using CistellAissam.Logicacistella;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json;
using System;


namespace CistellAissam.Controllers
{

    public class CistellController : Controller
    {
        public CistellRepo repo = new CistellRepo();
        public AccionsCistella accions = new AccionsCistella();
        [HttpGet]
        public IActionResult Index()
        {
           
            var productos = repo.ObtenirProductos();
            ViewData["contador"] = QuantitatCistella();

            return View("Cistell", productos);
        }

        [HttpPost]
        public IActionResult Afegir()
        {

            //Recuperar el code del produce desde el form
            var codeproducte = Request.Form["codeproducte"];
           
            accions.AddProducte(codeproducte);
            var productescistella = accions.GetCistella();
            int quantitatcista = 1;
            
            HttpContext.Session.SetString("productescistella", JsonSerializer.Serialize(productescistella));

            if (HttpContext.Session.GetInt32("Contador") != null)
            {
                quantitatcista = (int)HttpContext.Session.GetInt32("Contador") + 1;
            }
            HttpContext.Session.SetInt32("Contador", quantitatcista);

            return RedirectToAction("Index");
        }
        public IActionResult Cestill()
        {
            var productos = repo.ObtenirProductos();
            List<Producte> pr = new List<Producte>();
            //inicializar el variable para almacenar el precio total de productos del cistell
            var preuTotal = 0.0;
            //quantita del producte 
            int quantitat = 0;
            var productesSessionString = HttpContext.Session.GetString("productescistella");
            if (!string.IsNullOrEmpty(productesSessionString))
            {
                List<Cistella> productesSession = JsonSerializer.Deserialize<List<Cistella>>(productesSessionString);
                if (productesSession != null)
                {
                    foreach (var elem in productesSession)
                    {
                        var producte = productos.FirstOrDefault(p => p.codiProducte == elem.codeproducte);
                        if (producte != null)
                        {
                            double totalpreuproducte = Math.Round(elem.quantitat * producte.preuProducte,2);
                            ViewData["quantitat" + producte.codiProducte] = elem.quantitat;
                            ViewData["preuTotalt" + producte.codiProducte] = totalpreuproducte;
                            preuTotal += totalpreuproducte;
                            pr.Add(producte);
                        }
                    }
                }
            }
            ViewData["preuTotal"] = preuTotal;

            ViewData["contador"] = QuantitatCistella();

            return View("cistellCompra", pr);
        }
        public IActionResult ActualizarQuantitatCistell()
        {
            string codeproducte = Request.Form["codeproducte"];
            int novaquantitat = -1;
            if(!string.IsNullOrWhiteSpace(Request.Form["quantitat"]))
            {
                novaquantitat = int.Parse(Request.Form["quantitat"]);
            }
            var producte = HttpContext.Session.GetString(codeproducte);
            int quantitatProd = quantitatProducte(producte);
            if (novaquantitat != 0 && novaquantitat > 0 && novaquantitat is int && novaquantitat != null)
            {
                var producteSession = JsonSerializer.Deserialize<string[]>(HttpContext.Session.GetString(codeproducte));
                producteSession[1] = novaquantitat.ToString();
                string[] cistell = { codeproducte, producteSession[1] };
                HttpContext.Session.SetString(codeproducte, JsonSerializer.Serialize(cistell));
                this.actualitzarquntitatCistella(novaquantitat, quantitatProd);
            }
            else if(novaquantitat == 0)
            {
                this.Esborrarproducte(codeproducte);
                this.actualitzarquntitatCistella(novaquantitat, quantitatProd);
            }
            

            return RedirectToAction("Cestill");
        }

        public IActionResult FinalitzarCompra()
        {
            var productos = repo.ObtenirProductos();
            List<Producte> pr = new List<Producte>();
            //inicializar el variable para almacenar el precio total de productos del cistell
            var preuTotal = 0.0;
            //quantita del producte 
            int quantitat = 0;
            var productesSessionString = HttpContext.Session.GetString("productescistella");
            if (!string.IsNullOrEmpty(productesSessionString))
            {
                List<Cistella> productesSession = JsonSerializer.Deserialize<List<Cistella>>(productesSessionString);
                if (productesSession != null)
                {
                    foreach (var elem in productesSession)
                    {
                        var producte = productos.FirstOrDefault(p => p.codiProducte == elem.codeproducte);
                        if (producte != null)
                        {
                            double totalpreuproducte = Math.Round(elem.quantitat * producte.preuProducte, 2);
                            ViewData["quantitat" + producte.codiProducte] = elem.quantitat;
                            ViewData["preuTotalt" + producte.codiProducte] = totalpreuproducte;
                            preuTotal += totalpreuproducte;
                            pr.Add(producte);
                        }
                    }
                }
            }
            ViewData["preuTotal"] = preuTotal;

            ViewData["contador"] = QuantitatCistella();

            return View("FinalitzarCompra",pr);
        }

        public void Esborrarproducte(string productesession) {
             HttpContext.Session.Remove(productesession);
             
        }

        public int quantitatProducte(string ProducteSessio)
        {
            var productecistell = JsonSerializer.Deserialize<string[]>(ProducteSessio);
            return int.Parse(productecistell[1]);

        }
        public void actualitzarquntitatCistella(int novaquantitat,int quantitatProd)
        {
            int numproductescistell = (int)HttpContext.Session.GetInt32("Contador");
            if (numproductescistell != null)
            {
                if (novaquantitat > quantitatProd)
                {
                    var n = (numproductescistell - quantitatProd) + novaquantitat;
                    HttpContext.Session.SetInt32("Contador", n);
                }
                else
                {
                    var numCistell = quantitatProd - novaquantitat;
                    HttpContext.Session.SetInt32("Contador", numproductescistell - numCistell);
                }

            }
             
        }
        public void LimpiarqunatitatCistella()
        {
            HttpContext.Session.SetInt32("Contador", 0);
        }

        public int QuantitatCistella(){
            int numproductescistell = 0;
            if (HttpContext.Session.GetInt32("Contador") != null)
            {
                numproductescistell = (int)HttpContext.Session.GetInt32("Contador");
            }


           return numproductescistell;
        }
    }
}
