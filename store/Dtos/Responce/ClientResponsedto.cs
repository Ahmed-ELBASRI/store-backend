namespace store.Dtos.Responce
{
    public class ClientResponsedto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int FidalitePoints { get; set; } = 1;
        public bool IsActive { get; set; } = true;
    }
}
