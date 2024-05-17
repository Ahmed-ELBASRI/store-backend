using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class PhotoVarianteService : IPhotoVarianteService
    {
        private readonly StoreDbContext _context;

        public PhotoVarianteService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhotoVariante>> GetPhotosByVarianteIdAsync(int varianteId)
        {
            return await _context.photoVariantes
                .Where(p => p.VarianteId == varianteId)
                .ToListAsync();
        }
        public async Task<PhotoVariante> GetPhotosByPhotoIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id value. Id must be greater than 0.");
            }

            var photoVariante = await _context.photoVariantes.FindAsync(id);

            if (photoVariante == null)
            {
                throw new NotFoundException($"Photo Variante with ID {id} not found.");
            }

            return photoVariante;
        }

        public async Task<PhotoVariante> UploadPhotoAsync(int varianteId, string urlImage)
        {
            var photoVariante = new PhotoVariante
            {
                UrlImage = urlImage,
                VarianteId = varianteId
            };
            _context.photoVariantes.Add(photoVariante);
            await _context.SaveChangesAsync();
            return photoVariante;
        }
        public async Task UpdatePhotoUrlAsync(int photoId, PhotoVariante newPhotoVariante)
        {

            var photo = await _context.photoVariantes.FindAsync(photoId);
            if (photo == null)
            {
                throw new NotFoundException($"Photo with ID {photoId} not found.");
            }

            photo.UrlImage = newPhotoVariante.UrlImage;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            var photoVariante = await _context.photoVariantes.FindAsync(photoId);
            if (photoVariante == null)
            {
                throw new NotFoundException($"Photo with ID {photoId} not found.");
            }
            _context.photoVariantes.Remove(photoVariante);
            await _context.SaveChangesAsync();
        }
    }
}
