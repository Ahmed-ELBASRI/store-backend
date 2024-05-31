using Microsoft.EntityFrameworkCore;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class LignePanierService : ILignePanierService
    {
        private readonly StoreDbContext _context;

        public LignePanierService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<LignePanier>> GetLignesPanierByPanierId(int panierId)
        {
            var lignesPanier = await _context.LignePanier
                                             .Where(lp => lp.PanierId == panierId)
                                             .ToListAsync();

            return lignesPanier;
        }
        public async Task<LignePanier> GetLignePanier(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid id value, id must be greater than 0");
            }
            var lignePanier = await _context.LignePanier.FindAsync(id);

            if (lignePanier == null)
            {
                throw new ArgumentException($"attribute variante with id {id} not found");

            }
            return lignePanier;
        }

        public async Task<IEnumerable<LignePanier>> GetAllLignePaniers()
        {
            return await _context.LignePanier.ToListAsync();
        }

        public async Task<LignePanier> AddLignePanier(LignePanier lignePanier)
        {
            _context.LignePanier.Add(lignePanier);
            await _context.SaveChangesAsync();
            return lignePanier;
        }

        public async Task UpdateLignePanier(int id, LignePanier lignePanier)
        {
            if (id != lignePanier.Id)
            {
                throw new ArgumentException("ID mismatch");
            }

            _context.Entry(lignePanier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLignePanier(int id)
        {
            var lignePanier = await _context.LignePanier.FindAsync(id);
            if (lignePanier == null)
            {
                throw new KeyNotFoundException("LignePanier not found");
            }

            _context.LignePanier.Remove(lignePanier);
            await _context.SaveChangesAsync();
        }
    }
}
