using store.Models;

namespace store.Services.Contract
{
    public interface IProduitService
    {
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task DeleteProduct(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(int id, Product newProduct);
    }
}