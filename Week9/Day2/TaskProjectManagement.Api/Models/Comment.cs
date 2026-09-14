namespace TaskProjectManagement.Api.Models;

public class Comment
{
    public int Id { get; set; }

    public int TaskItemId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public TaskItem TaskItem { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}