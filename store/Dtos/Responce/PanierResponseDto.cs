using store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace store.Dtos.Responce
{
    public class PanierResponseDto
    {

        
        public int Id { get; set; }
        public Client? Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public IList<LignePanier> LPs { get; set; }

    }
}
