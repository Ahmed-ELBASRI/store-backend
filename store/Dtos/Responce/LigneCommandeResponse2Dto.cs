using System.ComponentModel.DataAnnotations;

namespace store.Dtos.Responce
{
    public class LigneCommandeResponse2Dto
    {

        [Key]
        public int IdLigneCommande { get; set; }
        public int Quantite { get; set; }
        public double ProduitUnitaire { get; set; }

        public string UrlImage {get;set;}

    }
}
