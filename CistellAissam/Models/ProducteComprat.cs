using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CistellAissam.Models
{
    public class ProducteComprat
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        [Required(ErrorMessage = "el Code del producte és obligatori")]
       
        public string ProducteCode { get; set; }
        public Producte Producte{ get; set; }
        public double Preu { get; set; }
        public int Quantitat { get; set; }

        public ProducteComprat()
        {
            this.Id = 0;
            this.Nom = String.Empty;
            this.Producte = new Producte();
            this.ProducteCode = String.Empty;
            this.Preu = 0;
            this.Quantitat = 0;
        }

        public ProducteComprat(int id, string nom, Producte producte, int preu, int quantitat)
        {
            this.Id = id;
            this.Nom = nom;
            this.Producte = producte;
            this.Preu = preu;
            this.Quantitat = quantitat;
        }

    }
}
