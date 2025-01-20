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
                imatgeproducte = "primerProducte.png",
                nomProducte = "primer Producte",
                preuProducte = 12 
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte ="segundoProducte.png",
                nomProducte = "Segon Producte",
                preuProducte = 12
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte = "tercerproducte.jpeg",
                nomProducte = "Tercer producte",
                preuProducte = 12
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123345",
                imatgeproducte = "quartProducte.jpg",
                nomProducte = "Quart",
                preuProducte = 12 
            });
        }
       
    }

}

