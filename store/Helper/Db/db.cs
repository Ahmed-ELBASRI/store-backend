using Microsoft.EntityFrameworkCore;
using store.Helper.Data;

namespace store.Helper.Db
{
    public class db : IDbHelper
    {
        public async Task<StoreDbContext> GetUserDbContextAsync(string ConnectionString)
        {
            // Configure DbContext options with the fetched connection string
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new StoreDbContext(options, ConnectionString);
        }
    }
}
