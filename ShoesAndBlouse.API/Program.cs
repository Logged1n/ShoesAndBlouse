using Microsoft.AspNetCore.Identity;
using ShoesAndBlouse.API.Extensions;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Infrastructure;
using ShoesAndBlouse.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clean Architecture Setup
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

//Enable Controllers
builder.Services.AddControllers()
    //dont wrap response with $id and $values
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
//TODO DI
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<PostgresDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations(); //TODO handle errors
}
//Comment out only for docker usage
//app.UseHttpsRedirection();


//Setup Controllers
app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();
