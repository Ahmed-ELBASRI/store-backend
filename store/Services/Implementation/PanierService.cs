using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Services.Implementation
{
    public class PanierService : IPanierService
    {
        private readonly StoreDbContext _context;

        public PanierService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Panier> GetPanierByClientId(int clientId)
        {
            if (clientId <= 0)
            {
                throw new ArgumentException("Invalid client id value, id must be greater than 0");
            }
            var panier = await _context.paniers
                                       .Include(p => p.LPs)
                                       .FirstOrDefaultAsync(p => p.ClientId == clientId);

            if (panier == null)
            {
                throw new ArgumentException($"Panier for client with id {clientId} not found");
            }

            return panier;
        }
        public async Task<Panier> GetPanier(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id value, id must be greater than 0");
            }

            var panier = await _context.paniers.FindAsync(id);

            if (panier == null)
            {
                throw new ArgumentException($"Panier with id {id} not found");
            }

            return panier;
        }

        public async Task<IEnumerable<Panier>> GetAllPaniers()
        {
            return await _context.paniers.ToListAsync();
        }

        public async Task<Panier> AddPanier(Panier panier)
        {
            _context.paniers.Add(panier);
            await _context.SaveChangesAsync();
            return panier;
        }

        public async Task UpdatePanier(int id, Panier panier)
        {
            if (id != panier.Id)
            {
                throw new ArgumentException("ID mismatch");
            }

            _context.Entry(panier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePanier(int id)
        {
            var panier = await _context.paniers.FindAsync(id);
            if (panier == null)
            {
                throw new KeyNotFoundException("Panier not found");
            }

            _context.paniers.Remove(panier);
            await _context.SaveChangesAsync();
        }
    }
}
