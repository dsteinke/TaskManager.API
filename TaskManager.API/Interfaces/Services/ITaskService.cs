using TaskManager.API.DTOs.Task;

namespace TaskManager.API.Interfaces.Services
{
    public interface ITaskService
    {
        Task<bool> CreateTask(TaskCreateDTO taskCreateDTO);
        Task<List<TaskDTO>> GetAllTasksForUser();
    }
}
