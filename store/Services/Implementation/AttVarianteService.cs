using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class AttVarianteService : IAttVarianteService
    {
        private readonly StoreDbContext _context;

        public AttVarianteService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Att_Variante> GetAttVarianteByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id value. Id must be greater than 0.");
            }

            var attVariante = await _context.att_variantes.FindAsync(id);

            if (attVariante == null)
            {
                throw new NotFoundException($"Attribute Variante with ID {id} not found.");
            }

            return attVariante;
        }

        public async Task<IEnumerable<Att_Variante>> GetAllAttVariantesAsync()
        {
            return await _context.att_variantes.ToListAsync();
        }

        public async Task<Att_Variante> CreateAttVarianteAsync(Att_Variante attVariante)
        {
            _context.att_variantes.Add(attVariante);
            await _context.SaveChangesAsync();
            return attVariante;
        }

        public async Task<Att_Variante> UpdateAttVarianteAsync(int id, Att_Variante updatedAttVariante)
        {
            var attVariante = await GetAttVarianteByIdAsync(id);
            attVariante.cle = updatedAttVariante.cle;
            attVariante.Valeur = updatedAttVariante.Valeur;
            // Update other properties as needed
            await _context.SaveChangesAsync();
            return attVariante;
        }

        public async Task DeleteAttVarianteAsync(int id)
        {
            var attVariante = await GetAttVarianteByIdAsync(id);
            _context.att_variantes.Remove(attVariante);
            await _context.SaveChangesAsync();
        }
    }
}
