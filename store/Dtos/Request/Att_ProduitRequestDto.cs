using store.Models;

namespace store.Dtos.Request
{
    public class Att_ProduitRequestDto
    {
        public String Cle { get; set; }
        public String Valeur { get; set; }
        //public Product? Produit { get; set; }
        public int productId { get; set; }
    }
}
