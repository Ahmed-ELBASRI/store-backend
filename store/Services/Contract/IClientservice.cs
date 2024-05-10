using store.Models;

namespace store.Services.Contract
{
    public interface IClientservice
    {
        Task<Client> GetClient(int id);
        Task<IEnumerable<Client>> GetAllClient();
        Task DesactivateClient(int id,bool cmd=false);
        Task AddClient(Client Client);
        Task UpdateClient(int id,Client newClient);
    }
}
