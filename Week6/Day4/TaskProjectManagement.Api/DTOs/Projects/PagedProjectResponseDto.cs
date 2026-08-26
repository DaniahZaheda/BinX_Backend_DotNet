namespace TaskProjectManagement.Api.DTOs.Projects;

public class PagedProjectResponseDto
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public List<ProjectResponseDto> Items { get; set; } = new();
}