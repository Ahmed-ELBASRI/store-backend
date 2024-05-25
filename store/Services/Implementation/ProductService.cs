
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

        public ProductService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(p => new ProductResponseDto { Id = p.Id, Name = p.Name, Description = p.Description, QteStock = p.QteStock, Prix = p.Prix });
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            return new ProductResponseDto { Id = product.Id, Name = product.Name, Description = product.Description, QteStock = product.QteStock, Prix = product.Prix };
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductRequestDto productRequestDto)
        {
            var product = new Product
            {
                Name = productRequestDto.Name,
                Description = productRequestDto.Description,
                QteStock = productRequestDto.QteStock,
                Prix = productRequestDto.Prix
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductResponseDto { Id = product.Id, Name = product.Name, Description = product.Description, QteStock = product.QteStock, Prix = product.Prix };
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