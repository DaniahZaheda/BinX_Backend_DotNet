using Microsoft.EntityFrameworkCore;
using Day3Lab.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("DefaultConnection"));
});


builder.Services.AddOpenApi();


var app = builder.Build();


app.MapControllers();


app.Run();