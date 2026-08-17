namespace IdentityDemo.Services;

public class TaskService
{
    public bool IsValidTaskTitle(string title)
    {
        return !string.IsNullOrWhiteSpace(title) && title.Length >= 3;
    }
}