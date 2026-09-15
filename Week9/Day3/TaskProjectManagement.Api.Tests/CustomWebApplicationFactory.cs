using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using TaskProjectManagement.Api.Data;

namespace TaskProjectManagement.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.AddSingleton<QueryCountingInterceptor>();

            var dbDescriptors = services
                .Where(d =>
                    d.ServiceType == typeof(ApplicationDbContext) ||
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>))
                .ToList();

            foreach (var descriptor in dbDescriptors)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var connectionString =
                    Environment.GetEnvironmentVariable("TEST_DB_CONNECTION")
                    ?? "Server=DANIAH;Database=TaskProjectManagementTestDb;Trusted_Connection=True;TrustServerCertificate=True";

                options.UseSqlServer(connectionString);

                options.AddInterceptors(
                    serviceProvider.GetRequiredService<QueryCountingInterceptor>());
            });

            var cacheDescriptors = services
                .Where(d => d.ServiceType == typeof(IDistributedCache))
                .ToList();

            foreach (var descriptor in cacheDescriptors)
            {
                services.Remove(descriptor);
            }

            services.AddDistributedMemoryCache();
        });
    }
}