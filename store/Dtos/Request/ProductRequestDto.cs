using store.Models;

namespace store.Dtos.Request
{
    public class ProductRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? QteStock { get; set; }
        public double? Prix { get; set; }
        public IFormFile? File { get; set; } // Use IFormFile to handle file uploads
    }
}
