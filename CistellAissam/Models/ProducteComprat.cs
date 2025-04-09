using System.ComponentModel.DataAnnotations;

namespace CistellAissam.Models
{
    public class ProducteComprat
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        [Required(ErrorMessage = "el producte del producte és obligatori")]
        public Producte producte{ get; set; }
        public string Preu { get; set; }
        public int Quantitat { get; set; }

        public ProducteComprat()
        {
            this.Id = 0;
            this.Nom = String.Empty;
            this.producte = new Producte();
            this.Preu = String.Empty;
            this.Quantitat = 0;
        }

        public ProducteComprat(int id, string nom, Producte producte, string preu, int quantitat)
        {
            this.Id = id;
            this.Nom = nom;
            this.producte = producte;
            this.Preu = preu;
            this.Quantitat = quantitat;
        }

    }
}
