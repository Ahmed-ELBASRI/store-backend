using Microsoft.EntityFrameworkCore;
using store.Helper.Data;
using store.Services.Contract;
using store.Services.Implementation;
using Stripe;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Chaine De Conx 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));
// This is just to build the DbContextOptions
//builder.Services.AddDbContext<StoreDbContext>(options =>
//    options.UseSqlServer("Initial connection string"));

// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Service
builder.Services.AddScoped<IProductService, store.Services.Implementation.ProductService>();
builder.Services.AddScoped<IClientservice, ClientService>();
builder.Services.AddScoped<IVarianteService, VarianteService>();
builder.Services.AddScoped<IAttVarianteService, AttVarianteService>();
builder.Services.AddScoped<IPhotoVarianteService, PhotoVarianteService>();
builder.Services.AddScoped<ILignePanierService, LignePanierService>();
builder.Services.AddScoped<IPanierService, PanierService>();
builder.Services.AddScoped<IPaiementservice, Paiementservice>();
builder.Services.AddScoped<IRetourservice, Retourservice>();
builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<ILigneCommandeService, LigneCommandeService>();
builder.Services.AddScoped<IAtt_ProduitService, Att_ProduitService>();
builder.Services.AddScoped<IPhotoProduitService, PhotoProduitService>();
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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SekretKey").Get<String>();

app.UseAuthorization();

app.MapControllers();

app.Run();
