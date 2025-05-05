using CistellAissam.Data;
using CistellAissam.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CistellAissam.Utils
{
    public class ProducteUtils
    {
        public static bool Add(Producte producte,TiendaContext _DBcontext)
        {
            if (_DBcontext.productes.FirstOrDefault(product => product.codiProducte == producte.codiProducte)== null)
            {
                _DBcontext.productes.Add(producte);
                _DBcontext.SaveChangesAsync();
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
