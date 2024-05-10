namespace store.Dtos.Request
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int QuantiteProduitStock { get; set; }
        public string Image { get; set; }  // a modifier 
    }
}
