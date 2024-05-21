using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;
using System.Net.Http.Headers;

namespace store.Services.Implementation
{
    public class PhotoProduitService : IPhotoProduitService
    {

        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public PhotoProduitService(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PhotoProduitResponseDto> GetPhotoProduitByIdAsync(int id)
        {
            var photoProduit = await _context.photoProduits.FindAsync(id);
            if (photoProduit == null)
                throw new NotFoundException($"PhotoProduit with ID {id} not found.");

            return _mapper.Map<PhotoProduitResponseDto>(photoProduit);
        }

        public async Task<IEnumerable<PhotoProduitResponseDto>> GetPhotosByProductIdAsync(int productId)
        {
            var photos = await _context.photoProduits.Where(p => p.ProductId == productId).ToListAsync();
            return _mapper.Map<IEnumerable<PhotoProduitResponseDto>>(photos);
        }
        private bool IsValidImageFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return false;
            }

            var validExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(filePath).ToLower();

            return validExtensions.Contains(fileExtension);
        }

        public async Task<PhotoProduitResponseDto> UploadPhotoAsync(int productId, string filePath)
        {
            if (!IsValidImageFile(filePath))
            {
                throw new ArgumentException("The provided file is not a valid image.");
            }

            var photoProduit = new PhotoProduit
            {
                ProductId = productId,
                UrlImage = filePath
            };

            _context.photoProduits.Add(photoProduit);
            await _context.SaveChangesAsync();

            var photoProduitResponseDto = _mapper.Map<PhotoProduitResponseDto>(photoProduit);
            return photoProduitResponseDto;
        }

        public async Task UpdatePhotoUrlAsync(int id, PhotoProduit newphotoProduit)
        {
            var photoProduit = await _context.photoProduits.FindAsync(id);
            if (photoProduit == null)
            {
                throw new NotFoundException($"Photo with ID {id} not found.");
            }
            if (!IsValidImageFile(newphotoProduit.UrlImage))
            {
                throw new ArgumentException("The provided file is not a valid image.");
            }

            photoProduit.ProductId = newphotoProduit.ProductId;
            photoProduit.UrlImage = newphotoProduit.UrlImage;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoProduitAsync(int id)
        {
            var photoProduit = await _context.photoProduits.FindAsync(id);
            if (photoProduit == null)
                throw new NotFoundException($"PhotoProduit with ID {id} not found.");

            _context.photoProduits.Remove(photoProduit);
            await _context.SaveChangesAsync();
        }
    }

}


    

