namespace store.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int QteStock { get; set; }
        public double Prix { get; set; }
        public IList<Variante> Variantes { get; set; }
        public IList<PhotoProduit> PPs { get; set; }
        public IList<Favorit> Favorits { get; set; }
    }
}
