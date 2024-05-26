using Asp.Versioning;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Identity Core Setup
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

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

//Add Identity Api Endpoints
builder.Services
    .AddIdentityApiEndpoints<User>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
        b.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//Session Management TODO DI Infrastructure
/*builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Comment out only for docker usage
//app.UseHttpsRedirection();

//app.UseSession();

//Allow to access static files from wwwroot folder
app.UseStaticFiles();

app.UseAuthorization();
app.UseAuthentication();

app.MapIdentityApi<User>();
app.MapControllers();
//Not secure for production!
app.UseCors("AllowAll");
app.UseRouting();

app.Run();
