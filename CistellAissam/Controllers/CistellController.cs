using CistellAissam.Repository;
using CistellAissam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Identity;
using CistellAissam.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace CistellAissam.Controllers
{

    public class CistellController : Controller
    {
         ProducteRepository repo = new();
        Cistelles cistella = new();
        public CistellController() { 
        }
        [HttpGet]
        public IActionResult Index()
        {
          
            var user = HttpContext.Session.GetString("usuarisession");
          
           
            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<Usuari>(user);
                ViewData["userauth"] = userauth;
            }
            var productos = repo.ObtenirProductos();
            ViewData["contador"] = QuantitatCistella();

            
            return View("Cistell", productos);
        }

        [HttpPost]

        public IActionResult Afegir(string codeproducte)
        {
            var user = HttpContext.Session.GetString("usuarisession");
            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<Usuari>(user);
                ViewData["userauth"] = userauth;
            }
            int quantitatcista = 1;
            var Cesta = SessionUtils.ObtenerProductosSession(HttpContext);
            if (Cesta != null)
            {
                cistella = new Cistelles(Cesta);
            }
            cistella.AddProducte(codeproducte);
            var productescistella = cistella.GetCistella();
            SessionUtils.GuardarProductosSession(HttpContext, productescistella);
            SessionUtils.IncrementarContadorCesta(HttpContext);
           
            return RedirectToAction("Index");
            
        }
        public IActionResult Cestill()
        {
            var user = HttpContext.Session.GetString("usuarisession");


            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<Usuari>(user);
                ViewData["userauth"] = userauth;
            }
            var productos = repo.ObtenirProductos();
            List<Producte> pr = new List<Producte>();
            var preuTotal = 0.0;
            int quantitat = 0;
            var productesSessionString = HttpContext.Session.GetString("productescistella");
            if (!string.IsNullOrEmpty(productesSessionString))
            {
                List<Cistella> productesSession = JsonSerializer.Deserialize<List<Cistella>>(productesSessionString);
                if (productesSession != null)
                {
                    foreach (var elem in productesSession)
                    {
                        var producte = repo.getProducte(elem.codeproducte);
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
            var user = HttpContext.Session.GetString("usuarisession");


            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<Usuari>(user);
                ViewData["userauth"] = userauth;
            }
            string codeproducte = Request.Form["codeproducte"];
            int novaquantitat = -1;
            if (HttpContext.Session.GetString("productescistella") != null)
            {
                List<Cistella> lista = JsonSerializer.Deserialize<List<Cistella>>(HttpContext.Session.GetString("productescistella"));
                cistella = new Cistelles(lista);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["quantitat"]))
            {
                novaquantitat = int.Parse(Request.Form["quantitat"]);
            }
           
            int quantitatProd = quantitatProducte(codeproducte);
            if (novaquantitat != 0 && novaquantitat > 0 && novaquantitat is int && novaquantitat != null)
            {
                cistella.actualizarQuantitat(codeproducte, novaquantitat);
                 this.actualitzarquntitatCistella(novaquantitat, quantitatProd);
            }
            else if(novaquantitat == 0)
            {
                cistella.EsborrarProducte(codeproducte);
                this.actualitzarquntitatCistella(novaquantitat, quantitatProd);
            }
            var productescistella = cistella.GetCistella();
            HttpContext.Session.SetString("productescistella", JsonSerializer.Serialize(productescistella));

            return RedirectToAction("Cestill");
        }
        public IActionResult FinalitzarCompra()
        {
            var user = HttpContext.Session.GetString("usuarisession");


            if (user != null)
            {
                var userauth = JsonSerializer.Deserialize<Usuari>(user);
                ViewData["userauth"] = userauth;
            }

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
                        var producte = repo.getProducte(elem.codeproducte);
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
            LimpiarqunatitatCistella();
            HttpContext.Session.Remove("productescistella");
            ViewData["contador"] = QuantitatCistella();

            return View("FinalitzarCompra",pr);
        }
        public int quantitatProducte(string ProducteSessio)
        {
             return cistella.getProducte(ProducteSessio).quantitat;
        }
        public void actualitzarquntitatCistella(int novaquantitat,int quantitatProd)
        {
            int numproductescistell = (int)HttpContext.Session.GetInt32("Contador");
            if (numproductescistell != null)
            {
                if (novaquantitat > quantitatProd){
                    var n = (numproductescistell - quantitatProd) + novaquantitat;
                    HttpContext.Session.SetInt32("Contador", n);
                }else{
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
