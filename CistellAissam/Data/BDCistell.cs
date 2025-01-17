using CistellAissam.Models;

namespace CistellAissam.Data
{
    public static class BDCistell
    {
        

  
       
            public static List<Producte> productes = new();


            static BDCistell()
        {
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte = "img",
                nomProducte = "primer Producte",
                preuProducte = 12 
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte = "img.png",
                nomProducte = "Segon Producte",
                preuProducte = 12
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte = "img2.png",
                nomProducte = "Tercer producte",
                preuProducte = 12
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte = "img3.png",
                nomProducte = "Quart",
                preuProducte = 12 
            });
        }
       
    }

}

