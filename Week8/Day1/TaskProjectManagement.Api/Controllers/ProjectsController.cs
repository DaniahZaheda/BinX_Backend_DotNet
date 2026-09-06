using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskProjectManagement.Api.Data;
using TaskProjectManagement.Api.DTOs.Projects;
using TaskProjectManagement.Api.Services;

namespace TaskProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ProjectService _projectService;

    public ProjectsController(
        ApplicationDbContext context,
        ProjectService projectService)
    {
        _context = context;
        _projectService = projectService;
    }

    // GET: /api/projects
    // Public endpoint
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedProjectResponseDto>> GetProjects(
        int page = 1,
        int pageSize = 10,
        string? search = null,
        string? ownerId = null,
        string? sort = "name")
    {
        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        var query = _context.Projects.AsQueryable();

        // ============================
        // Filtering
        // ============================

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search) ||
                (p.Description != null &&
                 p.Description.Contains(search)));
        }

        if (!string.IsNullOrWhiteSpace(ownerId))
        {
            query = query.Where(p => p.OwnerId == ownerId);
        }

        // ============================
        // Sorting
        // ============================

        switch (sort?.ToLower())
        {
            case "name":
                query = query.OrderBy(p => p.Name);
                break;

            case "name_desc":
                query = query.OrderByDescending(p => p.Name);
                break;

            case "newest":
                query = query.OrderByDescending(p => p.CreatedAt);
                break;

            case "oldest":
                query = query.OrderBy(p => p.CreatedAt);
                break;

            default:
                query = query.OrderBy(p => p.Name);
                break;
        }

        // ============================
        // Total Count
        // ============================

        var totalCount = await query.CountAsync();

        // ============================
        // Pagination + DTO
        // ============================

        var projects = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();

        // ============================
        // Response
        // ============================

        return Ok(new PagedProjectResponseDto
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = projects
        });
    }

    // POST: /api/projects
    // Authenticated users only
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProjectResponseDto>> CreateProject(
        CreateProjectDto dto)
    {
        // Get current authenticated user ID from JWT
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        try
        {
            var project = await _projectService.CreateProjectAsync(
                dto,
                userId);

            return CreatedAtAction(
                nameof(GetProjects),
                new { id = project.Id },
                project);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> DeleteProject(int id)
{
    var project = await _context.Projects.FindAsync(id);

    if (project == null)
        return NotFound();

    _context.Projects.Remove(project);
    await _context.SaveChangesAsync();

    return NoContent();
}


[HttpPut("{id}")]
[Authorize]
public async Task<IActionResult> UpdateProject(
    int id,
    CreateProjectDto dto)
{
    var userId = User.FindFirstValue(
        ClaimTypes.NameIdentifier);

    var project = await _context.Projects.FindAsync(id);

    if (project == null)
        return NotFound();

    if (project.OwnerId != userId &&
        !User.IsInRole("Admin"))
    {
        return Forbid();
    }

    project.Name = dto.Name;
    project.Description = dto.Description;
    project.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return Ok(new
    {
        message = "Project updated successfully."
    });
}
}