using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Helper.Db;
using store.Models;
using store.Services.Contract;

namespace store.Services.Implementation
{
    public class CommandService : ICommandService
    {
        private readonly StoreDbContext _context;
        private readonly IDbHelper db;

        public CommandService(StoreDbContext context , IDbHelper db)
        {
            _context = context;
            this.db = db;
        }

        public async Task AddCommand(Command Command)
        {
            await _context.AddAsync(Command);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommand(int id)
        {
            var command= await _context.Commands.FirstOrDefaultAsync(p => p.Id == id);
            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Command>> GetCommandsByClient(int clientId)
        {
            return await _context.Commands.Where(c => c.ClientId == clientId).ToListAsync();
        }
        public async Task<IEnumerable<Command>> GetAllCommand(string ConnectinString)
        {
            StoreDbContext dbContext = await this.db.GetUserDbContextAsync(ConnectinString);

            return await dbContext.Commands
                .Include(cl => cl.Client)
                .Include(c => c.LCs)
                .Include(lc => lc.Recs) 
                .Include(p => p.paiement)
                .ToListAsync();
        }

        public async Task<Command> GetCommandById(int id)
        {
            return await _context.Commands
                          .Include(cl => cl.Client)
                          .Include(c => c.LCs)
                          .Include(lc => lc.Recs)
                          .Include(p => p.paiement)
                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        /*public async Task UpdateCommand(int id, Command newCommand)
        {
            var commandToModifie = await _context.Commands.FirstOrDefaultAsync(p => p.Id == id);
            commandToModifie.Etat = newCommand.Etat;
            commandToModifie.Total = newCommand.Total;
            _context.Commands.Update(commandToModifie);
            await _context.SaveChangesAsync();
        }*/
        public async Task<bool> AnnulerCommande(int commandeId)
        {
            var commande = await _context.Commands.FindAsync(commandeId);

            if (commande == null)
            {
                return false;
            }

            if (commande.Etat == "Canceled")
            {
                return false;
            }
            commande.Etat = "Canceled";
            _context.Commands.Update(commande);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task <double> CalculerTotalCommande(int commandeId)
        {
            var commande = _context.Commands
                                   .Include(c => c.LCs)
                                   .ThenInclude(lc => lc.Variante)
                                   .FirstOrDefault(c => c.Id == commandeId);

            if (commande == null)
            {
                return 0.0;
            }
            double total = 0.0;
            foreach (var ligneCommande in commande.LCs)
            {
                total += ligneCommande.Quantite * ligneCommande.Variante.Prix;
            }
            return total;
        }
    }
}
