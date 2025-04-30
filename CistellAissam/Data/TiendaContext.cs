using Microsoft.EntityFrameworkCore;
using CistellAissam.Models;
using System.ComponentModel.DataAnnotations;
namespace CistellAissam.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options)
        {
        }

        public DbSet<Producte> productes { get; set; }
        public DbSet<Usuari> usuaris { get; set; }
        public DbSet<Venda> vendes { get; set; }
        public DbSet<UsuariLogin> usuariLogins { get; set; }
        public DbSet<ProducteComprat> productescomprats { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producte>().ToTable("Productes");
            modelBuilder.Entity<Usuari>().ToTable("Usuaris");
            modelBuilder.Entity<Venda>().ToTable("Vendas");
            modelBuilder.Entity<UsuariLogin>().ToTable("UsuarisLogins");
            modelBuilder.Entity<ProducteComprat>().ToTable("ProductesComprats");


            modelBuilder.Entity<Producte>().HasData(

             new Producte()
             {
                 codiProducte = "AIS123345",
                 imatgeproducte = "padadid.jpg",
                 nomProducte = "Raqueta Adidas Negra blanca",
                 preuProducte = 240
             },
           new Producte()
           {
               codiProducte = "AIS123346",
               imatgeproducte = "3pad1.png",
               nomProducte = "Raqueta Kombrt Negra Vermella",
               preuProducte = 190
           },
           new Producte()
           {
               codiProducte = "AIS123347",
               imatgeproducte = "4pad.jpg",
               nomProducte = "Raqueta wilson verde negre",
               preuProducte = 50
           },
           new Producte()
           {
               codiProducte = "AIS123348",
               imatgeproducte = "5padel.jpg",
               nomProducte = "Neceser adidas",
               preuProducte = 50
           },
           new Producte()
           {
               codiProducte = "AIS123349",
               imatgeproducte = "6pad.png",
               nomProducte = "Banda Simimum",
               preuProducte = 50
           }
           );
            modelBuilder.Entity<UsuariLogin>().HasData(
                new UsuariLogin("12345", "joan23@gmail.com", false, false, new DateTime(2025, 1, 12, 16, 23, 45)),
          new UsuariLogin("23456", "maria@gmail.com", false, false, new DateTime(2025, 1, 1, 23, 21, 5)),
           new UsuariLogin("34567", "carme17@gmail.com", false, false, new DateTime(2025, 2, 11, 20, 00, 23)),
            new UsuariLogin("45678", "super@gmail.com", true, false, new DateTime(2024, 1, 1, 0, 0, 0))
            );
            modelBuilder.Entity<Usuari>().HasData(
                new Usuari()
                {
                    Email = "joan23@gmail.com",
                    Nif ="01201201I"
                },
                 new Usuari()
                 {
                     Email = "maria@gmail.com",
                     Nif = "01201201I"
                 },
                 new Usuari()
                 {
                     Email = "carme17@gmail.com",
                     Nif = "01201201I"
                 }
                 ,
                 new Usuari()
                 {
                     Email = "super@gmail.com",
                     Nif = "01201201I"
                 }
                );

                 

    }

    }
}
