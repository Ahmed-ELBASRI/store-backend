using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V122.Network;
using store.Helper.Data;
using store.Helper.Db;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class PhotoVarianteService : IPhotoVarianteService
    {
        private readonly StoreDbContext _context;
        private readonly IDbHelper db;

        public PhotoVarianteService(StoreDbContext context,IDbHelper db)
        {
            _context = context;
            this.db = db;
        }

        public async Task<IEnumerable<PhotoVariante>> GetPhotosByVarianteIdAsync(int varianteId,string connectionString)
        {
            StoreDbContext dbContext = await this.db.GetUserDbContextAsync(connectionString);

            return await dbContext.photoVariantes
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
