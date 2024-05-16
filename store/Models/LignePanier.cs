using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class LignePanier
    {   
        public int Id { get; set; }
        public int Quantite { get; set; }
        public Panier? Panier { get; set; }
        [ForeignKey("Panier")]
        public int PanierId { get; set; }
        public Variante? Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
        //public object LPs { get; internal set; }
    }
}
