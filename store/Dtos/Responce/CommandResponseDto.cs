using store.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace store.Dtos.Responce
{
    public class CommandResponseDto
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public string Etat { get; set; }
        public double Total { get; set; }
        public int ClientId { get; set; }
    }
}
