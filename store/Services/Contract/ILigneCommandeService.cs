using store.Models;

namespace store.Services.Contract
{
    public interface ILigneCommandeService
    {
        Task<IEnumerable<LigneCommande>> GetAllLigneCommandes();
        Task<LigneCommande> GetLigneCommandeById(int id);
        Task AddLigneCommande(LigneCommande ligneCommande);
        Task UpdateLigneCommande(int id, LigneCommande newLigneCommande);
        Task DeleteLigneCommande(int id);
        Task<IEnumerable<LigneCommande>> GetLignesByCommandeId(int commandeId);
    }
}
