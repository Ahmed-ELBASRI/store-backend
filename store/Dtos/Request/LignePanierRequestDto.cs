using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Request
{
    public class LignePanierRequestDto
    {
        public int Quantite { get; set; }
        [ForeignKey("Panier")]
        public int PanierId { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
    }
}
