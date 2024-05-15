using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Request
{
    public class PanierRequestDto
    {
        public Client? Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public IList<LignePanier> LPs { get; set; }
    }
}
