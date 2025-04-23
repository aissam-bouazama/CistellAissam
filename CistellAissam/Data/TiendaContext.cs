using Microsoft.EntityFrameworkCore;
namespace CistellAissam.Models
{
    public class TiendaContext: DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options)
        {
        }

        public DbSet<Producte> productes { get; set; }
        public DbSet<Usuari> usuaris { get; set; }
        public DbSet<Venda> vendes { get; set; }
        public DbSet<ProducteComprat> productescomprats { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Producte>().ToTable("Producte");
            modelBuilder.Entity<Usuari>().ToTable("Usuari");
            modelBuilder.Entity<Venda>().ToTable("Venda");
            modelBuilder.Entity<ProducteComprat>().ToTable("ProducteComprat");

        }

    }
}
