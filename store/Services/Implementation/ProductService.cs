
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Helper.Db;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly StoreDbContext _context;
        private readonly IDbHelper dbHelper;

        public ProductService(StoreDbContext context, IDbHelper helper)
        {
            _context = context;
            this.dbHelper = helper;
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
        public async Task<ProductResponseDto> CreateProduct2Async(produitRequestDto productRequestDto)
        {
            var product = new Product
            {
                Name = productRequestDto.Name,
                Description = productRequestDto.Description,
                QteStock = productRequestDto.QteStock,
                Prix = productRequestDto.Prix
            };
            StoreDbContext db = await this.dbHelper.GetUserDbContextAsync(productRequestDto.chaineConnection);
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return new ProductResponseDto { Id = product.Id, Name = product.Name, Description = product.Description, QteStock = product.QteStock, Prix = product.Prix };
        }

        //public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductRequestDto productRequestDto)
        //{
        //    var product = await _context.Products.Include(p => p.PPs).FirstOrDefaultAsync(p => p.Id == id);
        //    if (product == null)
        //    {
        //        throw new NotFoundException($"Product with ID {id} not found.");
        //    }

        //    product.Name = productRequestDto.Name;
        //    product.Description = productRequestDto.Description;
        //    product.QteStock = productRequestDto.QteStock;
        //    product.Prix = productRequestDto.Prix;

        //    if (productRequestDto.File != null)
        //    {
        //        //var photoUrl = await _photoProduitService.UploadFileAsync(productRequestDto.File);

        //        if (product.PPs != null && product.PPs.Any())
        //        {
        //            product.PPs[0].UrlImage = photoUrl;
        //            _context.Entry(product.PPs[0]).State = EntityState.Modified;
        //        }
        //        else
        //        {
        //            product.PPs = new List<PhotoProduit>
        //    {
        //        new PhotoProduit { UrlImage = photoUrl }
        //    };
        //            _context.Entry(product.PPs[0]).State = EntityState.Added;
        //        }
        //    }

        //    _context.Entry(product).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return new ProductResponseDto
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Description = product.Description,
        //        QteStock = product.QteStock,
        //        Prix = product.Prix,
        //        Image = product.PPs?.FirstOrDefault()?.UrlImage
        //    };
        //}

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }

            var relatedVariantes = _context.att_variantes.Where(v => v.VarianteId == id);
            _context.att_variantes.RemoveRange(relatedVariantes);

            var relatedPhotoProduits = _context.photoProduits.Where(pp => pp.ProductId == id);
            _context.photoProduits.RemoveRange(relatedPhotoProduits);

            var relatedFavorits = _context.Favorits.Where(f => f.ProductId == id);
            _context.Favorits.RemoveRange(relatedFavorits);

            _context.Products.Remove(product);



            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetProductByVarianteIdAsync(int varianteId,string ConnectionString)
        {
            StoreDbContext dbContext = await this.dbHelper.GetUserDbContextAsync(ConnectionString);
            var variante = await dbContext.Variante.FindAsync(varianteId);
            if (variante == null)
            {
                throw new NotFoundException($"Variante with ID {varianteId} not found.");
            }

            var product = await dbContext.Products.FindAsync(variante.ProduitId);
            if (product == null)
            {
                throw new NotFoundException($"Product for variante with ID {varianteId} not found.");
            }

            return product;
        }
    }
}