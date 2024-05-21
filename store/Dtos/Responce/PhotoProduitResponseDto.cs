using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Responce
{
    public class PhotoProduitResponseDto
    {
        
        public int PhotoProduitId { get; set; }
        public String UrlImage { get; set; }
        public Product? Produit { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
