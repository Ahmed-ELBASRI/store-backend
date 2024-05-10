using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class Variante
    {
        [Key]
        public int IdVariante { get; set; }
        public int QteStock { get; set; }
        public double Prix { get; set; }
        public LignePanier LignesPanier { get; set; }
        public IList<PhotoVariante> PVs { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<Att_Variante> AttVs { get; set; }
        public IList<LigneCommande> LCs { get; set; }
        public Product Produit { get; set; }
        [ForeignKey("Produit")]
        public int ProduitId { get; set; }
    }
}
