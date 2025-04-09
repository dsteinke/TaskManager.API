using TaskManager.API.DTOs.Task;

namespace TaskManager.API.Interfaces.Services
{
    public interface ITaskService
    {
        Task<bool> CreateTask(TaskCreateDTO taskCreateDTO);
        Task<List<TaskDTO>> GetAllTasksForUser();
        Task<TaskDTO> GetTaskById(Guid taskId);

        Task<List<TaskDTO>> SearchTask(TaskSearchDTO taskSearchDTO);
        Task<bool> UpdateTask(Guid taskId, TaskUpdateDTO taskUpdateDTO);
        Task<bool> DeleteTask(Guid taskId);
    }
}
