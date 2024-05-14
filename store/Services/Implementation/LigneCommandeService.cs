using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class LigneCommandeService : ILigneCommandeService
    {
        private readonly StoreDbContext _context;

        public LigneCommandeService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LigneCommande>> GetAllLigneCommandes()
        {
                return await _context.ligneCommandes
                    .Include(lc => lc.Variante)
                    .Include(lc => lc.Commande)
                    .Include(lc => lc.retours)
                    .ToListAsync();
        }   
        public async Task<LigneCommande> GetLigneCommandeById(int id)
        {
            return await _context.ligneCommandes
                    .Include(lc => lc.Variante)
                    .Include(lc => lc.Commande)
                    .Include(lc => lc.retours)
                    .FirstOrDefaultAsync(lc => lc.IdLigneCommande == id);
        }
        public async Task AddLigneCommande(LigneCommande ligneCommande)
        {
            _context.ligneCommandes.Add(ligneCommande);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLigneCommande(int id)
        {
            var ligneCommandeToDelete = await _context.ligneCommandes.FirstOrDefaultAsync(lc => lc.IdLigneCommande == id);
            if (ligneCommandeToDelete != null)
            {
                _context.ligneCommandes.Remove(ligneCommandeToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateLigneCommande(int id, LigneCommande newLigneCommande)
        {
            if (newLigneCommande == null)
            {
                throw new ArgumentNullException(nameof(newLigneCommande), "The newLigneCommande object cannot be null.");
            }

            var ligneCommandeToUpdate = await _context.ligneCommandes.FirstOrDefaultAsync(lc => lc.IdLigneCommande == id);
            if (ligneCommandeToUpdate != null)
            {
                ligneCommandeToUpdate.Quantite = newLigneCommande.Quantite;
                ligneCommandeToUpdate.ProduitUnitaire = newLigneCommande.ProduitUnitaire;
                _context.ligneCommandes.Update(ligneCommandeToUpdate);
                await _context.SaveChangesAsync();
            }
        }

    }
}
