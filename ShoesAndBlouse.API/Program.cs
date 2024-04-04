using ShoesAndBlouse.Application;
using ShoesAndBlouse.Infrastructure;


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
}
//Comment out only for docker usage
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
