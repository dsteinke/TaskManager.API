using Task = TaskManager.API.Models.Task;

namespace TaskManager.API.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<int> CreateTask(Task task);
        Task<List<Task>> GetAllTasksForUser(Guid userId);
        Task<Task> GetTaskById(Guid taskId);
        Task<List<Task>> SearchTask(string title);
        Task<int> UpdateTask(Guid taskId);
        Task<int> DeleteTask(Guid taskId);
    }
}
