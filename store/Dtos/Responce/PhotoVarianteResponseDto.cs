using store.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Dtos.Responce
{
    public class PhotoVarianteResponseDto
    {
        [Key]
        public int IdPhoto { get; set; }
        public String? UrlImage { get; set; }
        public Variante? Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
    }
}
