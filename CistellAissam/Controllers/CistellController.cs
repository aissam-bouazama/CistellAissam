using CistellAissam.Data;
using CistellAissam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json;


namespace CistellAissam.Controllers
{

    public class CistellController : Controller
    {
        public CistellRepo repo = new CistellRepo();
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

            string[] cistell = { codeproducte, "1" };
            int quantitatcista = 1;
            int counproducts = 1;
            //convertir el produce i la quantitat a un json
            string jsonserialize = JsonSerializer.Serialize(cistell);
            if (HttpContext.Session.GetString(codeproducte) != null) {
                string producte = (string)HttpContext.Session.GetString(codeproducte);
                string[] productecistell = JsonSerializer.Deserialize<string[]>(producte);
                counproducts = int.Parse(productecistell[1]) + 1;
                string[] cistellambquantitat = { codeproducte, counproducts.ToString() };
                HttpContext.Session.SetString(codeproducte, JsonSerializer.Serialize(cistellambquantitat));
            }else
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
            var productos = repo.ObtenirProductos();
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
                    for (int i = 0; i < quantitat; i++)
                    {
                        preuTotal += producte.preuProducte;
                    }
                    //passar la quantitat por el view data 
                    ViewData["quantitat" + producte.codiProducte] = quantitat;
                    ViewData["preuTotalt" + producte.codiProducte] = quantitat * producte.preuProducte;
                    pr.Add(producte);
                }
            }
            //passar a la vista el precio total de productos del cistell
            ViewData["preuTotal"] = preuTotal;
            //passar la quantitat dels productes del cistell
            


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
           
            List<Producte> products = new List<Producte>();
            var productos = repo.ObtenirProductos();
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
                    for (int i = 0; i < quantitat; i++)
                    {
                        preuTotal += producte.preuProducte;
                    }
                    //passar la quantitat por el view data 
                    ViewData["quantitat" + producte.codiProducte] = quantitat;
                    ViewData["preuTotalt" + producte.codiProducte] = quantitat * producte.preuProducte;
                    pr.Add(producte);
                    this.Esborrarproducte(producte.codiProducte);
                }
            }
            //passar a la vista el precio total de productos del cistell
            ViewData["preuTotal"] = preuTotal;
            //passar la quantitat dels productes del cistell

          //  ViewData["contador"] = QuantitatCistella();
            LimpiarqunatitatCistella();

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
