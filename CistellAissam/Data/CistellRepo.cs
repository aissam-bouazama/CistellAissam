﻿using CistellAissam.Models;

namespace CistellAissam.Data
{
    public class CistellRepo
    {
        public Producte? getProducte(string codiproducte)
        {
            foreach (Producte pro in BDCistell.productes)
            {
                if (pro.codiProducte == codiproducte)
                {
                    return pro;
                }
            }
            return null;
        }
        public List<Producte>? ObtenirProductos()
        {
            return BDCistell.productes;
        }
       
        }
}
