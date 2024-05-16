using store.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Dtos.Request
{
    public class PhotoVarianteRequestDto
    {

        public String UrlImage { get; set; }
        public Variante? Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
    }
}
