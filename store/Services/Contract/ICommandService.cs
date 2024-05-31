using store.Models;

namespace store.Services.Contract
{
    public interface ICommandService
    {
        Task<IEnumerable<Command>> GetAllCommand(string ConnectinString);
        Task<Command?> GetCommandById(int id);
        Task AddCommand(Command Command);
        Task DeleteCommand(int id);
        Task<bool> AnnulerCommande(int commandeId);
        Task<double> CalculerTotalCommande(int commandeId);

        Task<IEnumerable<Command>> GetCommandsByClient(int clientId);
    }
    
}
