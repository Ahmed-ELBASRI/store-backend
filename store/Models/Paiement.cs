using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class Paiement
    {
        [Key]
        public int IdPaiement { get; set; }
        public DateTime DatePaimenet { get; set; }
        public double Montant { get; set; }
        public string modePaiement { get; set; }

        public Command commande { get; set; }
        [ForeignKey("Commande")]
        public int CommandeId { get; set; }
    }
}
