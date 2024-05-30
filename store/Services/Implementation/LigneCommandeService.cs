using Microsoft.EntityFrameworkCore;
using store.Dtos.Responce;
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
        public async Task<IEnumerable<LigneCommande>> GetLignesByCommandeId(int commandeId)
        {
            return await _context.ligneCommandes.Where(lc => lc.CommandeId == commandeId).ToListAsync();
        }
        public async Task<IEnumerable<LigneCommande>> GetLignessByCommandeId(int commandeId)
        {
            //return await _context.ligneCommandes.Where(lc => lc.CommandeId == commandeId).ToListAsync();
            var result = await _context.ligneCommandes
            .Where(lc => lc.CommandeId == commandeId)
            .Include(lc => lc.Variante) // Inclure la variante associée
            .ThenInclude(v => v.PVs) // Inclure les photos de la variante
            .Select(lc => new LigneCommandeResponseDto
            {
            IdLigneCommande = lc.IdLigneCommande,
            Quantite = lc.Quantite,
            ProduitUnitaire = lc.ProduitUnitaire,
            UrlImage = lc.Variante.PVs.FirstOrDefault().UrlImage // Prendre la première image (ou ajuster selon les besoins)
            })
        .ToListAsync();

            return (IEnumerable<LigneCommande>)result;
        }
        //public async Task<IEnumerable<LigneCommande>> GetLignessByCommandeId(int commandeId)
        //{
        //    return await _context.ligneCommandes.Where(lc => lc.CommandeId == commandeId).ToListAsync();

        //    var result = await _context.ligneCommandes
        //    .Where(lg => lg.CommandeId == commandeId)
        //    .Select(lg => new LigneCommande
        //    {
        //        IdLigneCommande = lg.IdLigneCommande,
        //        Quantite = lg.Quantite,
        //        ProduitUnitaire = lg.ProduitUnitaire,
        //        UrlImage = lg.(pv => pv.UrlImage).FirstOrDefault()
        //    })
        //    .ToListAsync();
        //}
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
