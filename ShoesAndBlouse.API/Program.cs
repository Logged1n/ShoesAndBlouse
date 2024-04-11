using ShoesAndBlouse.Application;
using ShoesAndBlouse.Infrastructure;
using ShoesAndBlouse.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clean Architecture Setup
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

//Decorator pattern for Caching repositories
//builder.Services.Decorate<IProductRepository, CachingProductRepository>();

//Redis Caching setup
//builder.Services.AddStackExchangeRedisCache(redisOptions =>
//{
 //   var connection = builder.Configuration.GetConnectionString("Redis");
 //   redisOptions.Configuration = connection;
//});

//Enable Controllers
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
//Comment out only for docker usage
//app.UseHttpsRedirection();

//Session Setup
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

//Setup Controllers
app.MapControllers();

app.Run();
