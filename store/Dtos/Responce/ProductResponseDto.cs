using store.Models;

namespace store.Dtos.Responce
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? QteStock { get; set; }
        public double? Prix { get; set; }
        public string Image { get; set; } // Store the file path or URL if needed
    }
}
