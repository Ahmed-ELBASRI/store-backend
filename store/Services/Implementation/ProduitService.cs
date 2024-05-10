
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class ProduitService : IProduitService
    {
        private readonly StoreDbContext _context;
        public ProduitService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProduct(int id, Product newProduct)
        {
            var productToModife = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            productToModife.Name = newProduct.Name;
            productToModife.QuantiteProduitStock = newProduct.QuantiteProduitStock;
            productToModife.Image = newProduct.Image;
            productToModife.Description = newProduct.Description;

            _context.Products.Update(productToModife);
            await _context.SaveChangesAsync();
        }
    }
}