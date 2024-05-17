using store.Models;

namespace store.Services.Contract
{
    public interface IVarianteService
    {
        Task<Variante> GetVarianteByIdAsync(int id);
        Task<IEnumerable<Variante>> GetAllVariantesAsync();
        Task<Variante> CreateVarianteAsync(Variante variante);
        Task<Variante> UpdateVarianteAsync(int id, Variante variante);
        Task DeleteVarianteAsync(int id);
    }
}
