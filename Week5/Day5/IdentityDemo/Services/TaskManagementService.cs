using IdentityDemo.Models;
using IdentityDemo.Repositories;

namespace IdentityDemo.Services;

public class TaskManagementService
{
    private readonly ITaskRepository _repository;

    public TaskManagementService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<string?> GetTaskTitleAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        return task?.Title;
    }

    public async Task<bool> UpdateTaskAsync(TaskItem task)
    {
        await _repository.UpdateAsync(task);

        return true;
    }
}