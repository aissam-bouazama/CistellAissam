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
    }
}
