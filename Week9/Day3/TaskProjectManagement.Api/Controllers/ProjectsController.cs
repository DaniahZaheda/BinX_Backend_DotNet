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

    /// <summary>
    /// Retrieves a paginated list of projects with optional search, owner, and sorting filters.
    /// </summary>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of projects per page.</param>
    /// <param name="search">Optional search text for project names or descriptions.</param>
    /// <param name="ownerId">Optional owner ID used to filter projects.</param>
    /// <param name="sort">Optional sorting criteria.</param>
    /// <response code="200">Returns a paginated list of projects.</response>
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

    /// <summary>
    /// Creates a new project for the authenticated user.
    /// </summary>
    /// <param name="dto">The project name and description.</param>
    /// <response code="201">Project created successfully.</response>
    /// <response code="400">The project data is invalid.</response>
    /// <response code="401">The user is not authenticated.</response>
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

    /// <summary>
    /// Deletes an existing project.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <response code="204">Project deleted successfully.</response>
    /// <response code="401">The user is not authenticated.</response>
    /// <response code="403">The user is not authorized to delete the project.</response>
    /// <response code="404">The project was not found.</response>
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

    /// <summary>
    /// Updates an existing project owned by the authenticated user or managed by an administrator.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="dto">The updated project information.</param>
    /// <response code="200">Project updated successfully.</response>
    /// <response code="401">The user is not authenticated.</response>
    /// <response code="403">The user is not authorized to update the project.</response>
    /// <response code="404">The project was not found.</response>
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

    /// <summary>
    /// Retrieves all projects with their owner information using eager loading.
    /// </summary>
    /// <response code="200">Returns a list of projects with owner information.</response>
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

    /// <summary>
    /// Retrieves projects with owner information using projection without eager loading.
    /// </summary>
    /// <response code="200">Returns a list of projects with owner information.</response>
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