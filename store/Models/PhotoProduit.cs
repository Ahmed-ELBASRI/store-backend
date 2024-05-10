namespace store.Models
{
    public class PhotoProduit
    {
        //IdProduit deja existe est ce qu'on doit faire IdPhoto au lieu de IdProduit??
        public int PhotoProduitId { get; set; }
        public String UrlImage { get; set; }
        public Product Produit { get; set; }
        public int ProductId { get; set; }
    }
}
