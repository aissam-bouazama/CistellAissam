using CistellAissam.Models;
using CistellAissam.Data;

namespace CistellAissam.Repository
{
    public class ProducteRepository
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
        public Boolean AfegirProducte(Producte producte)
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
    }
}
