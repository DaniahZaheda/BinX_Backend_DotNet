using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json;
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
    private readonly IDistributedCache _cache;

    public ProjectsController(
        ApplicationDbContext context,
        ProjectService projectService,
        IDistributedCache cache)
    {
        _context = context;
        _projectService = projectService;
        _cache = cache;
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

        // ============================
        // Cache Version
        // ============================

        var cacheVersion =
            await _cache.GetStringAsync("projects:version");

        if (cacheVersion == null)
        {
            cacheVersion = Guid.NewGuid().ToString();

            await _cache.SetStringAsync(
                "projects:version",
                cacheVersion);
        }

        // ============================
        // Cache Key
        // ============================

        var cacheKey =
            $"projects:v={cacheVersion}:page={page}:pageSize={pageSize}:search={search}:ownerId={ownerId}:sort={sort}";

        // ============================
        // Check Redis Cache
        // ============================

        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (cachedData != null)
        {
            Console.WriteLine("CACHE HIT");

            var cachedResult =
                JsonSerializer.Deserialize<PagedProjectResponseDto>(
                    cachedData);

            return Ok(cachedResult);
        }

        Console.WriteLine("CACHE MISS");

        // ============================
        // Database Query
        // ============================

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

        var result = new PagedProjectResponseDto
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = projects
        };

        // ============================
        // Save Result in Redis
        // ============================

        var serializedResult =
            JsonSerializer.Serialize(result);

        await _cache.SetStringAsync(
            cacheKey,
            serializedResult,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(10)
            });

        return Ok(result);
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

            // Invalidate projects cache
            await InvalidateProjectsCacheAsync();

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

    // DELETE: /api/projects/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        // Invalidate projects cache
        await InvalidateProjectsCacheAsync();

        return NoContent();
    }

    // PUT: /api/projects/{id}
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

        // Invalidate projects cache
        await InvalidateProjectsCacheAsync();

        return Ok(new
        {
            message = "Project updated successfully."
        });
    }

    // GET: /api/projects/with-owner
    // Eager loading with Include
    [HttpGet("with-owner")]
    [AllowAnonymous]
    public async Task<ActionResult<List<ProjectWithOwnerDto>>> GetProjectsWithOwner()
    {
        var projects = await _context.Projects
            .Include(p => p.Owner)
            .ToListAsync();

        var result = projects.Select(p => new ProjectWithOwnerDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CreatedAt = p.CreatedAt,
            OwnerName = p.Owner.FirstName + " " + p.Owner.LastName,
            OwnerEmail = p.Owner.Email
        }).ToList();

        return Ok(result);
    }

    // GET: /api/projects/projection
    // Projection without Include
    [HttpGet("projection")]
    [AllowAnonymous]
    public async Task<ActionResult<List<ProjectWithOwnerDto>>> GetProjectsProjection()
    {
        var projects = await _context.Projects
            .Select(p => new ProjectWithOwnerDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                OwnerName = p.Owner.FirstName + " " + p.Owner.LastName,
                OwnerEmail = p.Owner.Email
            })
            .ToListAsync();

        return Ok(projects);
    }

    // ============================
    // Cache Invalidation
    // ============================

    private async Task InvalidateProjectsCacheAsync()
    {
        var newVersion = Guid.NewGuid().ToString();

        await _cache.SetStringAsync(
            "projects:version",
            newVersion);
    }
}