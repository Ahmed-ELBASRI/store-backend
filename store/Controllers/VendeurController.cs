using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Helper.Data;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendeurController : ControllerBase
    {

        private readonly StoreDbContext _context;

        public VendeurController(StoreDbContext context)
        {
            this._context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DbGenerate(int id)
        {
            // Create user-specific database
            string userDatabaseName = "User_" + id; // Adjust as needed
            string createDatabaseQuery = $"CREATE DATABASE [{userDatabaseName}]";

            _context.Database.ExecuteSqlRaw(createDatabaseQuery);

            string connectionString = $"Data Source=.\\SQLEXPRESS;Initial Catalog={userDatabaseName};Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true;";


            // Configure database schema for user
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new StoreDbContext(options))
            {
                // Apply migrations for shared models
                context.Database.Migrate();
            }



            return Ok(connectionString);
        }

    }
}
