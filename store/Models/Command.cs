namespace store.Models
{
    public class Command
    {
        public int Id { get; set; }
        public List<Product> ProuductList { get; set; }
        public Client Name { get; set; }
        public bool IsDone { get; set; } = false;

    }
}