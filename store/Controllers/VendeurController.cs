using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Dtos.Request;
using store.Helper.Data;
using store.Helper.Db;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendeurController : ControllerBase
    {

        private readonly StoreDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IDbHelper helper;


        public VendeurController(StoreDbContext context, IConfiguration configuration , IDbHelper helper)
        {
            this._context = context;
            this._configuration = configuration;
            this.helper = helper;
        }

        [HttpGet("{id}")]
        public IActionResult DbGenerate(int id)
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

            string connectingString = this._configuration.GetConnectionString("DefaultConnection");

            using (var context = new StoreDbContext(options))
            {
                // Apply migrations for shared models
                context.Database.Migrate();
            }



            return Ok(connectionString);  //stock connection string in table vendeurs 
        }

        //[HttpPost]
        //public async Task<IActionResult> changeDebContext(DbRequestDto db)
        //{
        //    this.helper.
        //}

    }
}
