using Microsoft.EntityFrameworkCore;
using Pinzar_Daniela_Proiect.Models;

namespace Pinzar_Daniela_Proiect.Data
{
    public class MagazinContext:DbContext
    {
        public MagazinContext(DbContextOptions<MagazinContext> options) :base(options)
        {
        }

        public DbSet<Client> Clienti { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Distribuitor> Distribuitori { get; set; }
        public DbSet<DistribuitorProdus> DistribuitorProduse { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Comanda>().ToTable("Comanda");
            modelBuilder.Entity<Produs>().ToTable("Produs");
            modelBuilder.Entity<Distribuitor>().ToTable("Distribuitor");
            modelBuilder.Entity<DistribuitorProdus>().ToTable("DistribuitorProdus");
            modelBuilder.Entity<DistribuitorProdus>()
            .HasKey(c => new { c.ProdusID, c.DistribuitorID });//configureaza cheia primara compusa
        }


    }
}
