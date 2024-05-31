using store.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace store.Dtos.Responce
{
    public class PanierResponseDto
    {

        
        public int Id { get; set; }
        public Client? Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [JsonIgnore]
        public IList<LignePanier> LPs { get; set; }

    }
}
