using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Request
{
    public class CommandRequestDto
    {
        public DateTime DateCommande { get; set; }
        public string Etat { get; set; }
        public double Total { get; set; }
        public Client? Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public IList<LigneCommandeRequestDto>? LCs { get; set; }
        public IList<Reclamation>? Recs { get; set; }

        public Paiement? paiement { get; set; }
    }
}
