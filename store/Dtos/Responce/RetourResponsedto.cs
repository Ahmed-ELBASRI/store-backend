namespace store.Dtos.Responce
{
    public class RetourResponsedto
    {
        public int Id { get; set; }
        public DateTime? DateRetour { get; set; }
        public string? TypeRetour { get; set; }
        public string? Commentaire { get; set; }
        public int LigneCommandeId { get; set; }
    }
}
