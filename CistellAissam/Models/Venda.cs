using System.ComponentModel.DataAnnotations;

namespace CistellAissam.Models
{
    public class Venda
    {
        [Required(ErrorMessage = "El camp NFactura és obligatori")]
        public int NFactura { get; set; }
     
        public string Comprador { get; set; } //EL EMAIL DEL COMPRADOR
        public string Nif { get; set; }
        
        public string Nom { get; set; }
       
        public string Cognom { get; set; }
        
        public DateTime Data { get; set; }

        public ICollection<ProducteComprat> Productescomprats { get; set; }
        public Venda() {
            this.NFactura = 0;
            this.Nif = String.Empty;
            this.Nom = String.Empty;
            this.Cognom = String.Empty;
            this.Comprador = String.Empty;
            this.Productescomprats = new List<ProducteComprat>();

        }


        public Venda(int nFactura, string nif, string nom, string cognom, DateTime data)
        {
            this.NFactura = nFactura;
            this.Nif = nif;
            this.Nom = nom;
            this.Cognom = cognom;
            this.Data = data;
        }



    }
}
