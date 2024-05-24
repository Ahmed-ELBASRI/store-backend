using store.Models;

namespace store.Services.Contract
{
    public interface IPhotoVarianteService
    {
        Task<IEnumerable<PhotoVariante>> GetPhotosByVarianteIdAsync(int varianteId);
        Task<PhotoVariante> GetPhotosByPhotoIdAsync(int id);
        Task<PhotoVariante> UploadPhotoAsync(int varianteId, string urlImage);
        Task DeletePhotoAsync(int photoId);
        Task UpdatePhotoUrlAsync(int photoId, PhotoVariante newPhotoVariante);

    }
}
