using Microsoft.EntityFrameworkCore;
using Day3Lab.Models;

namespace Day3Lab.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public DbSet<Student> Students { get; set; }

    public DbSet<Book> Books { get; set; }
}