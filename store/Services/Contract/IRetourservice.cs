using store.Models;

namespace store.Services.Contract
{
    public interface IRetourservice
    {
        Task AddRetour(Retour retour);
        Task<IEnumerable<Retour>> GetAllRetours();
        Task<Retour> GetRetour(int id);
        Task<Retour> UpdateRetour(int id, Retour retour);
        Task DeleteRetour(int id);
    }
}
