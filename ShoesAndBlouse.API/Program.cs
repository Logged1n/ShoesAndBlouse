using Microsoft.AspNetCore.Identity;
using ShoesAndBlouse.API.Extensions;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure;
using ShoesAndBlouse.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

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

//Identity Core Setup TODO DI Infrastructure
builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services
    .AddIdentityCore<User>(options => 
        options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<PostgresDbContext>()
    .AddApiEndpoints();

//Session Management TODO DI Infrastructure
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations(); //TODO handle errors
}
//Comment out only for docker usage
//app.UseHttpsRedirection();
app.UseSession();
//Allow to access static files from wwwroot folder
app.UseStaticFiles();

app.MapIdentityApi<User>();
app.MapControllers();

//TODO move it to Domain
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole<int>>>();
    var roles = new[] { "Admin", "Salesman", "Client" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<int>(role));
    }
}

app.Run();
