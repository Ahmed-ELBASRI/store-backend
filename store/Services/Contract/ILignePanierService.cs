using store.Models;

namespace store.Services.Contract
{
    public interface ILignePanierService
    {
        Task<LignePanier> GetLignePanier(int id);
        Task<IEnumerable<LignePanier>> GetAllLignePaniers();
        Task<LignePanier> AddLignePanier(LignePanier lignePanier);
        Task UpdateLignePanier(int id, LignePanier lignePanier);
        Task DeleteLignePanier(int id);
        Task<List<LignePanier>> GetLignesPanierByPanierId(int panierId);
    }
}
