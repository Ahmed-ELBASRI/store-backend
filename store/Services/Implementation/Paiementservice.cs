using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;
using Stripe;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace store.Services.Implementation
{
    public class Paiementservice : IPaiementservice
    {
        private readonly StoreDbContext _context;
        private readonly IConfiguration _configuration;

        public Paiementservice(StoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task CreatePaiement(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            _context.SaveChanges();
        }

        public async Task DeletePaiement(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);
            if (paiement != null)
            {
                _context.Paiements.Remove(paiement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task <Paiement> GetPaiement(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);
            if (paiement == null)
            {
                throw new Exception("Paiement not found");
            }
            return paiement;
        }

        public async Task<IEnumerable<Paiement>> GetPaiements()
        {
            return await _context.Paiements.ToListAsync();
        }

        public async Task<Paiement> UpdatePaiement(int id, Paiement updatedpaiement)
        {
            var paiement = await GetPaiement(id);
            paiement.DatePaimenet = updatedpaiement.DatePaimenet;
            paiement.Montant = updatedpaiement.Montant;
            paiement.modePaiement = updatedpaiement.modePaiement;
            paiement.CommandeId = updatedpaiement.CommandeId;


            // Update other properties as needed
            await _context.SaveChangesAsync();
            return paiement;
        }
    }
}
