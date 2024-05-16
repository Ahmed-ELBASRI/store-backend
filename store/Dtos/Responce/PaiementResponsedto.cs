namespace store.Dtos.Responce
{
    public class PaiementResponsedto
    {
        public int IdPaiement { get; set; }
        public DateTime? DatePaimenet { get; set; }
        public double? Montant { get; set; }
        public string? ModePaiement { get; set; }
        public int CommandeId { get; set; }
    }
}
