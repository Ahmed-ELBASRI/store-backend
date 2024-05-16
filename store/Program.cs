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

//Chaine De Conx 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));

// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Service
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IClientservice, ClientService>();

builder.Services.AddScoped<IVarianteService, VarianteService>();
builder.Services.AddScoped<IAttVarianteService, AttVarianteService>();
builder.Services.AddScoped<IPhotoVarianteService, PhotoVarianteService>();


builder.Services.AddScoped<IPaiementservice, Paiementservice>();
builder.Services.AddScoped<IRetourservice, Retourservice>();




builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<ILigneCommandeService, LigneCommandeService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
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
