
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly StoreDbContext _context;
        private readonly IPhotoProduitService _photoProduitService;
        public ProductService(StoreDbContext context, IPhotoProduitService photoProduitService)
        {
            _context = context;
            _photoProduitService = photoProduitService;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.Include(p =>p.PPs).ToListAsync();

            var t = products.Select(p => new ProductResponseDto { Id = p.Id, Name = p.Name, Description = p.Description, QteStock = p.QteStock, Prix = p.Prix, Image = p.PPs.FirstOrDefault() != null ? p.PPs.FirstOrDefault().UrlImage.Split(new string[] { "\\src" }, StringSplitOptions.None)[1] : string.Empty });
            return products.Select(p => new ProductResponseDto { Id = p.Id, Name = p.Name, Description = p.Description, QteStock = p.QteStock, Prix = p.Prix, Image = p.PPs.FirstOrDefault() != null ? p.PPs.FirstOrDefault().UrlImage.Split(new string[] { "\\src" }, StringSplitOptions.None)[1] : string.Empty });
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            return new ProductResponseDto { Id = product.Id, Name = product.Name, Description = product.Description, QteStock = product.QteStock, Prix = product.Prix, Image = product.PPs.FirstOrDefault() != null ? product.PPs.FirstOrDefault().UrlImage.Split(new string[] { "/src" }, StringSplitOptions.None)[1] : string.Empty };
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductRequestDto productRequestDto)
        {
            if (productRequestDto.File != null)
            {
                var photoP = new List<PhotoProduit>
        {
            new PhotoProduit
            {
                UrlImage = await _photoProduitService.UploadFileAsync(productRequestDto.File)
            }
        };
                var product = new Product
                {
                    Name = productRequestDto.Name,
                    Description = productRequestDto.Description,
                    QteStock = productRequestDto.QteStock,
                    Prix = productRequestDto.Prix,
                    PPs = photoP,
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return new ProductResponseDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    QteStock = product.QteStock,
                    Prix = product.Prix
                };
            }
            else
            {
                var product = new Product
                {
                    Name = productRequestDto.Name,
                    Description = productRequestDto.Description,
                    QteStock = productRequestDto.QteStock,
                    Prix = productRequestDto.Prix,
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return new ProductResponseDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    QteStock = product.QteStock,
                    Prix = product.Prix
                };
            }
        }

        public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductRequestDto productRequestDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            product.Name = productRequestDto.Name;
            product.Description = productRequestDto.Description;
            product.QteStock = productRequestDto.QteStock;
            product.Prix = productRequestDto.Prix;
            await _context.SaveChangesAsync();
            return new ProductResponseDto { Id = product.Id, Name = product.Name, Description = product.Description, QteStock = product.QteStock, Prix = product.Prix };
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}