using TaskManager.API.DTOs.Task;
using Task = TaskManager.API.Models.Task;

namespace TaskManager.API.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<int> CreateTask(Task task);
        Task<List<Task>> GetAllTasksForUser(Guid userId);
        Task<Task?> GetTaskById(Guid taskId);
        Task<List<Task>> SearchTask(Guid userId, TaskSearchDTO taskSearchDTO);
        Task<int> UpdateTask(Guid taskId, Guid userId, TaskUpdateDTO taskUpdateDTO);
        Task<int> DeleteTask(Guid taskId, Guid userId);
    }
}
