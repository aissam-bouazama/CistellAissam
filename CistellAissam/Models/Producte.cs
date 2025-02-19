using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace CistellAissam.Models
{
    public class Producte
    {
        [Required(ErrorMessage="el codi del producte és obligatori")]
        public string codiProducte { get; set; }
        [Required(ErrorMessage = "el nom del producte és obligatori")]
        public string nomProducte { get; set; }
        [Required(ErrorMessage = "el Preu del producte és obligatori")]
        public double preuProducte { get; set; }
       // [Required(ErrorMessage = "Imatge del producte és obligatoria")]
        public string imatgeproducte { get; set; }   
    }
}
