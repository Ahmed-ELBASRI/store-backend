using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Helper.Data;
using store.Helper.Db;
using store.Models;
using store.Services.Contract;
using System.Runtime.CompilerServices;

namespace store.Services.Implementation
{
    public class VarianteService : IVarianteService
    {
        private readonly StoreDbContext _context;
        private readonly IDbHelper db;

        public VarianteService(StoreDbContext context, IDbHelper db)
        {
            _context = context;
            this.db = db;
        }

        public async Task<Variante> GetVarianteByIdAsync(int id,String connectionString)
        {

            StoreDbContext dbContext = await this.db.GetUserDbContextAsync(connectionString);
            var variante = await dbContext.Variante.FindAsync(id);
            if (variante == null)
            {
                throw new NotFoundException($"Variante with ID {id} not found.");
            }
            return variante;
        }

        public async Task<IEnumerable<Variante>> GetAllVariantesAsync()
        {
            return await _context.Variante.ToListAsync();
        }

        public async Task<Variante> CreateVarianteAsync(Variante variante)
        {
            _context.Variante.Add(variante);
            await _context.SaveChangesAsync();
            return variante;
        }

        //public async Task<Variante> UpdateVarianteAsync(int id, Variante updatedVariante)
        //{
        //    var variante = await GetVarianteByIdAsync(id);
        //    variante.QteStock = updatedVariante.QteStock;
        //    variante.Prix = updatedVariante.Prix;
        //    // Update other properties as needed
        //    await _context.SaveChangesAsync();
        //    return variante;
        //}

        //public async Task DeleteVarianteAsync(int id)
        //{
        //    var variante = await GetVarianteByIdAsync(id);
        //    _context.Variante.Remove(variante);
        //    await _context.SaveChangesAsync();
        //}
    }
}
