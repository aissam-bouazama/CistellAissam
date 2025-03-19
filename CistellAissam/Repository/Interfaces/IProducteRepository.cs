using CistellAissam.Models;

namespace CistellAissam.Repository.Interfaces
{
    public interface IProducteRepository
    {
        bool AfegirProducte(Producte producte);
        Producte? getProducte(string codiproducte);
        List<Producte>? ObtenirProductos();
    }
}