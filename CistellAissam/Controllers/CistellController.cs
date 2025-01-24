﻿using CistellAissam.Data;
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
            
            string[] cistell = [codeproducte, "1"];
            int quantitatcista = 1;
            int counproducts = 1;
            string jsonserialize = JsonSerializer.Serialize(cistell);
            if(HttpContext.Session.GetString(codeproducte)!=null){
                string producte = (string)HttpContext.Session.GetString(codeproducte);
                string[] productecistell = JsonSerializer.Deserialize<string[]>(producte);
                counproducts = int.Parse(productecistell[1])+1;
            }
            else
            {
                if (HttpContext.Session.GetInt32("Contador") != null)
                {
                    quantitatcista = (int)HttpContext.Session.GetInt32("Contador") + 1;
                }
                HttpContext.Session.SetString(codeproducte, jsonserialize);

            }

          
            
 

            HttpContext.Session.SetInt32("Contador", quantitatcista);
            
            return RedirectToAction("Index");
        }
    }
}
