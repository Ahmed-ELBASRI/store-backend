using System.ComponentModel.DataAnnotations.Schema;

namespace store.Models
{
    public class Command
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public string Etat { get; set; }
        public double Total { get; set; }
        public Client Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public IList<LigneCommande> LCs { get; set; }
        public IList<Reclamation> Recs { get; set; }

        public Paiement paiement { get; set; }

    }
}