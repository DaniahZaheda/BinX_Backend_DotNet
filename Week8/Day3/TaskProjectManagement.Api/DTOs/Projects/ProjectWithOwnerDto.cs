namespace TaskProjectManagement.Api.DTOs.Projects;

public class ProjectWithOwnerDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public string OwnerName { get; set; } = string.Empty;

    public string? OwnerEmail { get; set; }
}