using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class Att_ProduitService : IAtt_ProduitService
    {
        private readonly StoreDbContext _context;

        public Att_ProduitService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAtt_Produit(Att_Produit Att_Produit)
        {
            await _context.AddAsync(Att_Produit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAtt_Produit(int id)
        {
            var att_produit = await _context.att_produits.FirstOrDefaultAsync(p => p.Id == id);

            if (att_produit != null)
            {
                _context.att_produits.Remove(att_produit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Att_Produit>> GetAllAtt_Produit()
        {
            return await _context.att_produits.ToListAsync();
        }

        public async Task<Att_Produit> GetAtt_Produit(int id)
        {
            return await _context.att_produits.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAtt_Produit(int id, Att_Produit newAtt_Produit)
        {
            var existingAttProduct = await _context.att_produits.FirstOrDefaultAsync(p => p.Id == id);

            if (existingAttProduct == null)
            {
                throw new KeyNotFoundException("Att Product not found");
            }

            existingAttProduct.Cle = newAtt_Produit.Cle;
            existingAttProduct.Valeur = newAtt_Produit.Valeur;
            existingAttProduct.ProductId = newAtt_Produit.ProductId;

            await _context.SaveChangesAsync();
        }
    }
}
