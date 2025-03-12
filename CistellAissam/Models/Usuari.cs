﻿using System.ComponentModel.DataAnnotations;

namespace CistellAissam.Models
{
    public class Usuari
    {
        /// <summary>email. Serveix com a login. Ha de ser únic.</summary>
        [Required(ErrorMessage ="el email és Obligatori")]
        [EmailAddress(ErrorMessage = "El usuari  ha de ser de format Email.")]
        public string email { get; set; }

        /// <summary>Password. Es guarda sense cap encriptaió</summary>
        [Required(ErrorMessage = "Has de posar una Contrasenya")]
        public string password { get; set; }
        
        /// <summary>Si es true es un administrador.</summary>
        public bool isAdmin { get; set; }

        /// <summary>Si es true, l'usuari esta bloquejat i no pot fer login</summary>
        public bool locked { get; set; }

        /// <summary>Data y hora de creació o darrera edició o bloqueig</summary>
        public DateTime lastupdate { get; set; }

        public Usuari(string password, string email, bool isAdmin, bool locked, DateTime lastupdate)
        {
            this.password = password;
            this.email = email;
            this.isAdmin = isAdmin;
            this.locked = locked;
            this.lastupdate = lastupdate;
        }

        public Usuari() { 
        this.email=string.Empty;
        this.password=string.Empty;
        }
    }
}
