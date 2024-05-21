using Asp.Versioning;
using ShoesAndBlouse.API.Extensions;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

//Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clean Architecture Setup
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

//Setup ApiVersioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

//Enable Controllers
builder.Services.AddControllers()
    //dont wrap response with $id and $values
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

//Identity Core Setup
builder.Services.AddAuthorization();
//builder.Services.AddAuthentication()
//    .AddCookie(IdentityConstants.ApplicationScheme)
//    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services
    .AddIdentityApiEndpoints<User>();

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

//Inits roles on new instance of database
await app.InitRolesAsync();

app.Run();
