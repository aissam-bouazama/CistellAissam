using CistellAissam.Models;
using CistellAissam.Data;

namespace CistellAissam.Repository
{
    public class CistellRepository
    {
        public Producte? getProducte(string codiproducte)
        {
            foreach (Producte pro in Productes.productes)
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
            return Productes.productes;
        }
        public bool AfegirProducte(Producte producte)
        {
            if (getProducte(producte.codiProducte) == null)
            {
                Productes.productes.Add(producte);
                return true;

            }
            else
            {
                return false;
            }
        }
        public bool EliminarProducte(string codiproducte)
        {
            Producte? producte = getProducte(codiproducte);
            if (producte != null)
            {
                Productes.productes.Remove(producte);
                return true;
            }
            else{
                return false;
            }
        }  
        public Producte? ModificarProducte(Producte producte)
        {
            Producte? producte1 = getProducte(producte.codiProducte);
            if (producte1 != null)
            { 
               int index = Productes.productes.IndexOf(producte1);
                Productes.productes[index].nomProducte = producte.nomProducte;
                Productes.productes[index].preuProducte = producte.preuProducte;
                Productes.productes[index].imatgeproducte = producte.imatgeproducte;
                return producte1;
            } else {
                return null;
            }
        }
    }
}
