using CistellAissam.Models;

namespace CistellAissam.Repository.Interfaces
{
    public interface ICistellRepository
    {
        bool AfegirProducte(Producte producte);
        bool EliminarProducte(string codiproducte);
        Producte? getProducte(string codiproducte);
        Producte? ModificarProducte(Producte producte);
        List<Producte>? ObtenirProductos();
    }
}