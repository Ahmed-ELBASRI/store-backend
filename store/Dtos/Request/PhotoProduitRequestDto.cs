using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Request
{
    public class PhotoProduitRequestDto
    {
        public String UrlImage { get; set; }
        public Product? Produit { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
