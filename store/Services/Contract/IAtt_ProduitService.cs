using store.Models;

namespace store.Services.Contract
{
    public interface IAtt_ProduitService
    {
        Task<Att_Produit> GetAtt_Produit(int id);
        Task<IEnumerable<Att_Produit>> GetAllAtt_Produit();
        Task DeleteAtt_Produit(int id);
        Task AddAtt_Produit(Att_Produit Att_Produit);
        Task UpdateAtt_Produit(int id, Att_Produit newAtt_Produit);

    }
}
