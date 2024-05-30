namespace store.Dtos.Request
{
    public class produitRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? QteStock { get; set; }
        public double? Prix { get; set; }

        public string chaineConnection { get; set; }
    }
}
