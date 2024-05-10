using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class ClientService : IClientservice
    {
        private readonly StoreDbContext _context;

 

        public ClientService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddClient(Client Client)
        {
            await _context.AddAsync(Client);
            await _context.SaveChangesAsync();
        }

        public async Task DesactivateClient(int id, bool cmd)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(p => p.Id == id);
            client.IsActive = cmd;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClient()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClient(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateClient(int id, Client newClient)
        {
            var ClientModife = await _context.Clients.FirstOrDefaultAsync(p => p.Id == id);
                ClientModife.Username = newClient.Username;
                ClientModife.FidalitePoints = newClient.FidalitePoints;
                ClientModife.Email = newClient.Email;
                ClientModife.Password=newClient.Password;
                ClientModife.FidalitePoints=newClient.FidalitePoints;
            

                _context.Clients.Update(ClientModife);
                 await _context.SaveChangesAsync();
        }
    }
}
