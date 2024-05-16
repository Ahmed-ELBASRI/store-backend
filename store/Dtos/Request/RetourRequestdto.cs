namespace store.Dtos.Request
{
    public class RetourRequestdto
    {
        public DateTime? DateRetour { get; set; }
        public string? TypeRetour { get; set; }
        public string? Commentaire { get; set; }
        public int LigneCommandeId { get; set; }
    }
}