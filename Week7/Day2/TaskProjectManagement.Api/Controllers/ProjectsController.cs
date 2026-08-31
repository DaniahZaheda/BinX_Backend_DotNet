using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet]
    public async Task<ActionResult<PagedProjectResponseDto>> GetProjects(
        int page = 1,
        int pageSize = 10,
        string? search = null,
        string? ownerId = null,
        string? sort = "name")
    {
        // Validate pagination
        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        // Start query
        var query = _context.Projects.AsQueryable();

        // ============================
        // Filtering
        // ============================

        // Filter 1: Search by name or description
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search) ||
                (p.Description != null &&
                 p.Description.Contains(search)));
        }

        // Filter 2: Filter by owner
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
        // Pagination + DTO Projection
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
    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> CreateProject(
        CreateProjectDto dto)
    {
        // Use the seeded system user as the project owner
        var userId = "11111111-1111-1111-1111-111111111111";

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
}