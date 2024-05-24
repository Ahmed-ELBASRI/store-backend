using store.Models;

namespace store.Dtos.Request
{
    public class ProductRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? QteStock { get; set; }
        public double? Prix { get; set; }
        public IList<Variante>? Variantes { get; set; }
        public IList<PhotoProduit>? PPs { get; set; }
        public IList<Favorit>? Favorits { get; set; }
    }
}
