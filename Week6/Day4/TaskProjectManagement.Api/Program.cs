using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TaskProjectManagement.Api.Data;
using TaskProjectManagement.Api.Models;
using TaskProjectManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ============================
// Database
// ============================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ============================
// Identity
// ============================

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ============================
// Services
// ============================

builder.Services.AddScoped<ProjectService>();

// ============================
// Controllers & OpenAPI
// ============================

builder.Services.AddControllers();

builder.Services.AddOpenApi();

// ============================
// Build Application
// ============================

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }