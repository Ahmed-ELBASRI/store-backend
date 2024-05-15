namespace store.Dtos.Request
{
    public class PaiementRequestdto
    {
        public DateTime? DatePaimenet { get; set; }
        public double? Montant { get; set; }
        public string? ModePaiement { get; set; }
        public int CommandeId { get; set; }    }
}
