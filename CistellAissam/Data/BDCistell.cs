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
                imatgeproducte = "padadid.jpg",
                nomProducte = "Raqueta Adidas Negra blanca",
                preuProducte = 240
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123346",
                imatgeproducte = "3pad1.png",
                nomProducte = "Raqueta Kombrt Negra Vermella",
                preuProducte =190
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123347",
                imatgeproducte = "4pad.jpg",
                nomProducte = "Raqueta wilson verde negre",
                preuProducte = 50 
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123348",
                imatgeproducte = "5padel.jpg",
                nomProducte = "Neceser adidas",
                preuProducte = 50
            });
            productes.Add(new Producte()
            {
                codiProducte = "AIS123349",
                imatgeproducte = "6pad.png",
                nomProducte = "Banda Simimum",
                preuProducte = 50
            });

            

        }
       
    }

}

