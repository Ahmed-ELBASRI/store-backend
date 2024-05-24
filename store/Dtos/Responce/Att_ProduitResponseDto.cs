using store.Models;

namespace store.Dtos.Responce
{
    public class Att_ProduitResponseDto
    {
        public int Id { get; set; }
        public String Cle { get; set; }
        public String Valeur { get; set; }
        public Product? Produit { get; set; }
        public int ProductId { get; set; }
    }
}
