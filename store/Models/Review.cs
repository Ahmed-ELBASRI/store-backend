using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Commentaire { get; set; }
        public int Note { get; set; }

        public Client Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Variante Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }

    }
}
