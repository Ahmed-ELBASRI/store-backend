namespace store.Dtos.Request
{
    public class ClientRequestRegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Adresse { get; set; }
        public int PointsFidelite { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public string ConnectionString { get; set; }


    }
}
