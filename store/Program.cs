using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Services.Contract;
using store.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Chaine De Conx 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));
// This is just to build the DbContextOptions
//builder.Services.AddDbContext<StoreDbContext>(options =>
//    options.UseSqlServer("Initial connection string"));

// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Service
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IClientservice, ClientService>();
builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<ILigneCommandeService, LigneCommandeService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "http://localhost:4300")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();
// Enable CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();