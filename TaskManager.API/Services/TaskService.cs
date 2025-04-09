using AutoMapper;
using System.Security.Claims;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces.Repositories;
using TaskManager.API.Interfaces.Services;
using Task = TaskManager.API.Models.Task;

namespace TaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public TaskService
            (ITaskRepository taskRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        private Guid GetUserId()
        {
            var userId =
                _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(userId, out var guid) ? guid : throw new UnauthorizedAccessException("Invalid token");
        }

        public async Task<bool> CreateTask(TaskCreateDTO taskCreateDTO)
        {
            var userId = GetUserId();

            try
            {
                var task = _mapper.Map<Task>(taskCreateDTO);

                task.UserId = userId.ToString();

                await _taskRepository.CreateTask(task);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TaskDTO>> GetAllTasksForUser()
        {
            var userId = GetUserId();

            var tasks = await _taskRepository.GetAllTasksForUser(userId);

            var result = _mapper.Map<List<TaskDTO>>(tasks);

            return result;
        }

        public async Task<TaskDTO> GetTaskById(Guid taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);

            var result = _mapper.Map<TaskDTO>(task);

            return result;
        }

        public async Task<List<TaskDTO>> SearchTask(TaskSearchDTO taskSearchDTO)
        {
            var userId = GetUserId();

            var tasks = await _taskRepository.SearchTask(userId, taskSearchDTO);

            var result = _mapper.Map<List<TaskDTO>>(tasks);

            return result;
        }

        public async Task<bool> UpdateTask(Guid taskId, TaskUpdateDTO taskUpdateDTO)
        {
            try
            {
                var userId = GetUserId();

                var result = await _taskRepository.UpdateTask(taskId, userId, taskUpdateDTO);

                if (result == 0)
                    throw new UnauthorizedAccessException("Task not found or access denied");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTask(Guid taskId)
        {
            try
            {
                var userId = GetUserId();

                var result = await _taskRepository.DeleteTask(taskId, userId);

                if (result == 0)
                    throw new UnauthorizedAccessException("Task not found or access denied");

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
