using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Request
{
    public class LigneCommandeRequestDto
    {
        public int Quantite { get; set; }
        public double ProduitUnitaire { get; set; }
        public Variante? Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
        public Command? Commande { get; set; }
        [ForeignKey("Commande")]
        public int CommandeId { get; set; }

        public IList<Retour>? retours { get; set; }
    }
}
