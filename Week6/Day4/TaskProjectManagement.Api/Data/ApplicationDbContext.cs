using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskProjectManagement.Api.Models;

namespace TaskProjectManagement.Api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ============================
        // Project
        // ============================

        builder.Entity<Project>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Entity<Project>()
            .Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Entity<Project>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


        // ============================
        // ProjectMember
        // ============================

        builder.Entity<ProjectMember>()
            .HasKey(pm => new
            {
                pm.ProjectId,
                pm.UserId
            });

        builder.Entity<ProjectMember>()
            .Property(pm => pm.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project)
            .WithMany(p => p.Members)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProjectMember>()
            .HasOne(pm => pm.User)
            .WithMany()
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        // ============================
        // TaskItem
        // ============================

        builder.Entity<TaskItem>()
            .Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Entity<TaskItem>()
            .Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TaskItem>()
            .HasOne(t => t.AssignedTo)
            .WithMany()
            .HasForeignKey(t => t.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull);


        // ============================
        // Comment
        // ============================

        builder.Entity<Comment>()
            .Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Entity<Comment>()
            .HasOne(c => c.TaskItem)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        // ============================
        // Seed Data
        // ============================

        var seedUserId = "11111111-1111-1111-1111-111111111111";

        var seedUser = new ApplicationUser
        {
            Id = seedUserId,
            UserName = "admin@taskmanagement.com",
            NormalizedUserName = "ADMIN@TASKMANAGEMENT.COM",

            Email = "admin@taskmanagement.com",
            NormalizedEmail = "ADMIN@TASKMANAGEMENT.COM",
            EmailConfirmed = true,

            FirstName = "System",
            LastName = "Admin",

            SecurityStamp =
                "22222222-2222-2222-2222-222222222222",


 ConcurrencyStamp =
        "33333333-3333-3333-3333-333333333333",
        
            PasswordHash =
                "AQAAAAIAAYagAAAAEJQ0nXQ4uK0V0l8v3X7fX8p7z9j8X3Z8Y9R6Q7P5Y4T3S2R1Q0P9O8N7M6L5K4J3H2G1F0"
        };

        builder.Entity<ApplicationUser>()
            .HasData(seedUser);


        // ============================
        // Seed Projects
        // ============================

        builder.Entity<Project>()
            .HasData(
                new Project
                {
                    Id = 1,
                    Name = "Website Development",
                    Description =
                        "A project for developing a company website.",

                    OwnerId = seedUserId,

                    CreatedAt = new DateTime(2026, 8, 24),

                    UpdatedAt = null
                },

                new Project
                {
                    Id = 2,
                    Name = "Mobile Application",
                    Description =
                        "A project for developing a mobile application.",

                    OwnerId = seedUserId,

                    CreatedAt = new DateTime(2026, 8, 24),

                    UpdatedAt = null
                }
            );
    }
}