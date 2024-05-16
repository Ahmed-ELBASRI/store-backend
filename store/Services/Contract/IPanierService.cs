using store.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace store.Services.Contract
{
    public interface IPanierService
    {
        Task<Panier> GetPanier(int id);
        Task<IEnumerable<Panier>> GetAllPaniers();
        Task<Panier> AddPanier(Panier panier);
        Task UpdatePanier(int id, Panier panier);
        Task DeletePanier(int id);
    }
}
