using Microsoft.AspNetCore.Identity;

namespace ShoesAndBlouse.API.Extensions;

public static class RolesExtensions
{
    public static async Task InitRolesAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole<int>>>();
        var roles = new[] { "Admin", "Salesman", "Client" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}