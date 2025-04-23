using System.ComponentModel.DataAnnotations;

namespace CistellAissam.Models
{
    public class Producte
    {
        [Required(ErrorMessage = "el codi del producte és obligatori")]
        [Key]
        public string codiProducte { get; set; }
        [Required(ErrorMessage = "el nom del producte és obligatori")]
        public string nomProducte { get; set; }
        [Required(ErrorMessage = "el Preu del producte és obligatori")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El preu del producte ha de ser més gran que 0.")]
        public double preuProducte { get; set; }
        [Required(ErrorMessage = "L'imatge del producte és obligatòria.")]
        public string? imatgeproducte { get; set; }

    }
}
