using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class LigneCommande
    {
        [Key]
        public int IdLigneCommande { get; set; }
        public int Quantite { get; set; }
        public double ProduitUnitaire { get; set; }
        public Variante Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
        public Command Commande { get; set; }
        [ForeignKey("Commande")]
        public int CommandeId { get; set; }

        public IList<Retour> retours { get; set; }
    }
}
