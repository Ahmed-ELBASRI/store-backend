using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class Favorit
    {
        [Key]
        public int IdFavoris { get; set; }
        public Client Client { get; set; }

        public int ClientId { get; set; }

        public Product Produit { get; set; }

        public int ProductId { get; set; }
    }
}
