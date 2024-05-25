using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using store.Helper.Data;
using store.Services.Contract;
using store.Services.Implementation;
using store.Settings;
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

// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Service
builder.Services.AddScoped<IProduitService, ProduitService>();
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure Stripe settings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));




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

// Configure Stripe API Key
var stripeSettings = app.Services.GetRequiredService<IOptions<StripeSettings>>().Value;
StripeConfiguration.ApiKey = stripeSettings.SecretKey;

app.UseAuthorization();

app.MapControllers();

app.Run();
