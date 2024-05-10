using Microsoft.EntityFrameworkCore;
using store.Models;

namespace store.Helper.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Retour> Retours { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Paiement>Paiements { get; set; }
        public virtual DbSet<Variante> Variante { get; set; }
        public virtual DbSet<LignePanier> LignePanier { get; set; }
        public virtual DbSet<Review> reviews { get; set; }
        public virtual DbSet<Reclamation> Reclamations { get; set; }
        public virtual DbSet<PhotoVariante> photoVariantes { get; set; }
        public virtual DbSet<PhotoProduit> photoProduits { get; set; }
         public virtual DbSet<Panier> paniers { get; set; }
        public virtual DbSet<LigneCommande> ligneCommandes { get; set; }
        public virtual DbSet<Favorit> Favorits { get; set; }
        public  virtual DbSet<Att_Variante> att_variantes { get; set; }
        public  virtual DbSet<Att_Produit> att_produits { get; set; }

    }
}
