using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Helper.Db;
using store.Models;
using store.Services.Contract;
using Stripe;

namespace store.Services.Implementation
{
    public class ClientService : IClientservice
    {
        private readonly StoreDbContext _context;
        private readonly IDbHelper db;



        public ClientService(StoreDbContext context,IDbHelper db)
        {
            _context = context;
            this.db = db;
        }

        public async Task<Client> VerfiyLogin(Client cl,string ConnectionString)
        {
            StoreDbContext dbContext = await this.db.GetUserDbContextAsync(ConnectionString);
            var client = await dbContext.Clients.FirstOrDefaultAsync(v=> v.Email.Equals(cl.Email) && v.Password.Equals(cl.Password));
            return client;
        }
        public async Task RegisterClient(Client cl,string ConnectionString)
        {
            cl.Adresse = "oujda";
            StoreDbContext dbContext = await this.db.GetUserDbContextAsync(ConnectionString);
            await dbContext.AddAsync(cl);
            await dbContext.SaveChangesAsync();

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
            ClientModife.Adresse = newClient.Adresse;
            ClientModife.Email = newClient.Email;
            ClientModife.Password = newClient.Password;
            _context.Clients.Update(ClientModife);
            await _context.SaveChangesAsync();
        }
    }
}
