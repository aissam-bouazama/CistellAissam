using Microsoft.EntityFrameworkCore;
using CistellAissam.Models;
namespace CistellAissam.Data
{
    public class TiendaContext: DbContext
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

        }

    }
}
