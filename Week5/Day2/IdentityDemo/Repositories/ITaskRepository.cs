using IdentityDemo.Models;

namespace IdentityDemo.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    Task UpdateAsync(TaskItem task);
}