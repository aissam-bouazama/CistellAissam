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
        [ForeignKey(nameof(producte))]
        public string productecodiProducte { get; set; }
        public Producte producte{ get; set; }

        public double Preu { get; set; }
        public int Quantitat { get; set; }
        [ForeignKey(nameof(Venda))]
        public int VendaNFactura { get; set; }
        public Venda Venda { get; set; }

        public ProducteComprat()
        {
            this.Id = 0;
            this.Nom = String.Empty;
            this.productecodiProducte = String.Empty;
            this.Preu = 0;
            this.Quantitat = 0;
        }

        public ProducteComprat(int id, string nom,string producteCode, int preu, int quantitat)
        {
            this.Id = id;
            this.Nom = nom;
            this.productecodiProducte = producteCode;
            this.Preu = preu;
            this.Quantitat = quantitat;
        }

    }
}
