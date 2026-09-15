using Microsoft.AspNetCore.Identity;
using TaskProjectManagement.Api.Models;

namespace TaskProjectManagement.Api.Data;

public static class IdentitySeeder
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public static async Task SeedAsync(IServiceProvider services)
    {
        await _semaphore.WaitAsync();

        try
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            var admin =
                await userManager.FindByEmailAsync(
                    "admin@taskmanagement.com");

            if (admin != null &&
                !await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(
                    admin,
                    "Admin");
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}