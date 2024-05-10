namespace store.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int FidalitePoints { get; set; } = 1;
        public bool IsActive { get; set; } = true;
    }
}
