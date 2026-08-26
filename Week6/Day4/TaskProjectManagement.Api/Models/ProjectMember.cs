namespace TaskProjectManagement.Api.Models;

public class ProjectMember
{
    public int ProjectId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public DateTime JoinedAt { get; set; }

    public string Role { get; set; } = "Member";

    public Project Project { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}