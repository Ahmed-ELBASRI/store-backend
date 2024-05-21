using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;

namespace store.Services.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductRequestDto productRequestDto);
        Task<ProductResponseDto> UpdateProductAsync(int id, ProductRequestDto productRequestDto);
        Task DeleteProductAsync(int id);
    }
}