
using Microsoft.EntityFrameworkCore;
using TaskProjectManagement.Api.Models;

namespace TaskProjectManagement.Api.Data;

public static class TestDataSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context =
            services.GetRequiredService<ApplicationDbContext>();

        // Don't add data if test projects already exist
        if (await context.Projects.CountAsync() >= 50)
            return;

        var seedUserId =
            "11111111-1111-1111-1111-111111111111";

        var projects = new List<Project>();

        for (int i = 3; i <= 52; i++)
        {
            projects.Add(new Project
            {
                Name = $"Test Project {i}",
                Description = $"Test project number {i}",
                OwnerId = seedUserId,
                CreatedAt = DateTime.UtcNow.AddDays(-i)
            });
        }

        await context.Projects.AddRangeAsync(projects);
        await context.SaveChangesAsync();
    }
}
