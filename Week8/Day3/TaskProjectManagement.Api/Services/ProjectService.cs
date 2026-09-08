using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskProjectManagement.Api.Data;
using TaskProjectManagement.Api.DTOs.Projects;
using TaskProjectManagement.Api.Models;

namespace TaskProjectManagement.Api.Services;

public class ProjectService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProjectService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<ProjectResponseDto> CreateProjectAsync(
        CreateProjectDto dto,
        string userId)
    {
        // Check if the project name already exists for this owner
        var existingProject = await _context.Projects
            .AnyAsync(p =>
                p.OwnerId == userId &&
                p.Name.ToLower() == dto.Name.ToLower());

        if (existingProject)
        {
            throw new InvalidOperationException(
                "You already have a project with this name.");
        }

        // Get the owner
        var owner = await _userManager.FindByIdAsync(userId);

        if (owner == null)
        {
            throw new InvalidOperationException(
                "User not found.");
        }

        // Start transaction
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            // Create project
            var project = new Project
            {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim(),
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            // Add owner as project member
            var projectMember = new ProjectMember
            {
                ProjectId = project.Id,
                UserId = userId,
                Role = "Owner"
            };

            _context.ProjectMembers.Add(projectMember);

            await _context.SaveChangesAsync();

            // Commit transaction
            await transaction.CommitAsync();

            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}