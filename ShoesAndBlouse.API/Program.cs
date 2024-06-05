using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

using ShoesAndBlouse.Application;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shoes and Blouse API", Version = "v1" });
    c.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
    {
        Name = ".AspNetCore.Identity.Application",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Cookie,
        Description = "ASP.NET Core Identity Cookie"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "cookieAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});

//Identity Core Setup
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = ".AspNetCore.Identity.Application";
    });
builder.Services.AddAuthorization();

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
}
//Comment out only for docker usage
//app.UseHttpsRedirection();

app.UseSession();

//Allow to access static files from wwwroot folder
app.UseStaticFiles();

//Not secure for production!
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();
app.MapControllers();

app.Run();
