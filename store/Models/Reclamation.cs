using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace store.Models
{
    public class Reclamation
    {

        [Key]
        public int Id { get; set; }
        public String Description { get; set; }
        public String Libelle { get; set; }
        public Command Commande { get; set; }

        [ForeignKey("Commande")]
        public int CommandeId { get; set; }
    }
}
