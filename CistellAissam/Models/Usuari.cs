using System.ComponentModel.DataAnnotations;

namespace CistellAissam.Models
{
    public class Usuari
    {
        [Required(ErrorMessage = "el email és obligatori")]
        [Key]
        [EmailAddress(ErrorMessage = "El email ha de ser de format Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "el Nif és obligatori")]

        [MaxLength(9, ErrorMessage = "El Nif ha de tenir 9 caràcters")]
        public string Nif { get; set; }

        public string Nom { get; set; }
        public string Cognom { get; set; }



        public string Telefon { get; set; }
 
      
       
     
        public DateTime DataNaixement { get; set; }

        public Usuari(string nom, string cognom, string email, string telefon, string pais, DateTime dataNaixement)
        {
            this.Nom = nom;
            this.Cognom = cognom;
            this.Email = email;
            this.Telefon = telefon;
     
            this.DataNaixement = dataNaixement;
        }
        public Usuari()
        {
            this.Nom = string.Empty;
            this.Cognom = string.Empty;
            this.Email = string.Empty;
            
            this.Telefon = string.Empty;
          
            
            this.DataNaixement = DateTime.Now;
        }
    }
}
