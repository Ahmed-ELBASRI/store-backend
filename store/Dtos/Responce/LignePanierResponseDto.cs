using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Responce
{
    public class LignePanierResponseDto
    {
        public int Id { get; set; }
        public int Quantite { get; set; }
        [ForeignKey("Panier")]
        public int PanierId { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
    }
}
