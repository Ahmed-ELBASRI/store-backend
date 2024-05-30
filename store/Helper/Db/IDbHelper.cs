using store.Helper.Data;

namespace store.Helper.Db
{
    public interface IDbHelper
    {
        Task<StoreDbContext> GetUserDbContextAsync(string ConnectionString);
    }
}
