using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace store.Services.Implementation
{
    public class Retourservice : IRetourservice
    {
        private readonly StoreDbContext _context;

        public Retourservice(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddRetour(Retour retour)
        {
            await _context.Retours.AddAsync(retour);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRetour(int id)
        {
            var retour = await _context.Retours.FindAsync(id);
            if (retour != null)
            {
                _context.Retours.Remove(retour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Retour>> GetAllRetours()
        {
            return await _context.Retours.ToListAsync();
        }

        public async Task<Retour> GetRetour(int id)
        {
            var retour = await _context.Retours.FindAsync(id);
            if (retour == null)
            {
                throw new Exception("Retour not found");
            }
            return retour;
        }

        public async Task<Retour> UpdateRetour(int id, Retour updatedretour)
        {
            var retour = await GetRetour(id);
            retour.DateRetour = updatedretour.DateRetour;
            retour.TypeRetour = updatedretour.TypeRetour;
            retour.Commentaire = updatedretour.Commentaire;
            retour.LigneCommandeId = updatedretour.LigneCommandeId;


            // Update other properties as needed
            await _context.SaveChangesAsync();
            return retour;
        }
        private bool RetourExists(int id)
        {
            return _context.Retours.Any(e => e.Id == id);
        }

    }
}
