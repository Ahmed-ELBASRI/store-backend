using store.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace store.Dtos.Responce
{
    public class LigneCommandeResponseDto
    {
        [Key]
        public int IdLigneCommande { get; set; }
        public int Quantite { get; set; }
        public double ProduitUnitaire { get; set; }
        public string UrlImage { get; set; }
        [JsonIgnore]
        public Variante Variante { get; set; }
        [ForeignKey("Variante")]
        public int VarianteId { get; set; }
        [JsonIgnore]
        public Command Commande { get; set; }
        [ForeignKey("Commande")]
        public int CommandeId { get; set; }
        [JsonIgnore]
        public IList<Retour> retours { get; set; }
    }
}
