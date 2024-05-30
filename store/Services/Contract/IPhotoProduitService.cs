using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;

namespace store.Services.Contract
{
    public interface IPhotoProduitService
    {
        Task<PhotoProduitResponseDto> GetPhotoProduitByIdAsync(int id);
        Task<IEnumerable<PhotoProduitResponseDto>> GetPhotosByProductIdAsync(int productId);
        Task<PhotoProduitResponseDto> UploadPhotoAsync(int productId, string filePath);
        Task UpdatePhotoUrlAsync(int id, PhotoProduit produit);
        Task DeletePhotoProduitAsync(int id);
        Task<string> UploadFileAsync(IFormFile file);
    }
    
}
