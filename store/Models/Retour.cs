namespace store.Models
{
    public class Retour
    {
        public int Id { get; set; }
        public DateTime DateRetour { get; set; }
        public string TypeRetour { get; set; }
        public string Commentaire { get; set; }

        public LigneCommande LigneCommande { get; set; }
        public int LigneCommandeId { get; set; }

    }
}
