using CistellAissam.Models;
using CistellAissam.Data;

namespace CistellAissam.Repository
{
    public class CistellRepository
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
        public Boolean AfegirProducte(Producte producte)
        {
            if (getProducte(producte.codiProducte) == null)
            {
                BDCistell.productes.Add(producte);
                return true;

            }
            else
            {
                return false;
            }


        }
        public Boolean EliminarProducte(string codiproducte)
        {
            Producte? producte = getProducte(codiproducte);
            if (producte != null)
            {
                BDCistell.productes.Remove(producte);
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
               int index =  BDCistell.productes.IndexOf(producte1);
                BDCistell.productes[index].nomProducte = producte.nomProducte;
                BDCistell.productes[index].preuProducte = producte.preuProducte;
                BDCistell.productes[index].imatgeproducte = producte.imatgeproducte;
                return producte1;
            } else {
                return null;
            }
        }
    }
}
