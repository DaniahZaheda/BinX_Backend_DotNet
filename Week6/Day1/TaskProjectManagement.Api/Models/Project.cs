namespace TaskProjectManagement.Api.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string OwnerId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ApplicationUser Owner { get; set; } = null!;

    public ICollection<ProjectMember> Members { get; set; }
        = new List<ProjectMember>();

    public ICollection<TaskItem> Tasks { get; set; }
        = new List<TaskItem>();
}