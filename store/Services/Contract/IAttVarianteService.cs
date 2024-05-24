using store.Models;

namespace store.Services.Contract
{
    public interface IAttVarianteService
    {
        Task<Att_Variante> GetAttVarianteByIdAsync(int id);
        Task<IEnumerable<Att_Variante>> GetAllAttVariantesAsync();
        Task<Att_Variante> CreateAttVarianteAsync(Att_Variante attVariante);
        Task<Att_Variante> UpdateAttVarianteAsync(int id, Att_Variante attVariante);
        Task DeleteAttVarianteAsync(int id);
    }
}
