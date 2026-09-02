namespace TaskProjectManagement.Api.Models;

public class TaskItem
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string? AssignedToId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskStatus Status { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Project Project { get; set; } = null!;

    public ApplicationUser? AssignedTo { get; set; }

    public ICollection<Comment> Comments { get; set; }
        = new List<Comment>();
}