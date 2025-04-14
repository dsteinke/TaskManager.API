using AutoMapper;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces.Repositories;
using TaskManager.API.Interfaces.Services;
using Task = TaskManager.API.Models.Task;

namespace TaskManager.API.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public TaskService
            (ITaskRepository taskRepository, IMapper mapper, IUserService userService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<bool> CreateTask(TaskCreateDTO taskCreateDTO)
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var task = _mapper.Map<Task>(taskCreateDTO);

            task.UserId = userId.ToString();

            await _taskRepository.CreateTask(task);

            return true;
        }

        public async Task<List<TaskDTO>> GetAllTasksForUser()
        {
            var userId = _userService.GetUserId_LoggedInUser();

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
            var userId = _userService.GetUserId_LoggedInUser();

            var tasks = await _taskRepository.SearchTask(userId, taskSearchDTO);

            var result = _mapper.Map<List<TaskDTO>>(tasks);

            return result;
        }

        public async Task<bool> UpdateTask(Guid taskId, TaskUpdateDTO taskUpdateDTO)
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var result = await _taskRepository.UpdateTask(taskId, userId, taskUpdateDTO);

            if (result == 0)
                throw new UnauthorizedAccessException("Task not found or access denied");

            return true;
        }

        public async Task<bool> DeleteTask(Guid taskId)
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var result = await _taskRepository.DeleteTask(taskId, userId);

            if (result == 0)
                throw new UnauthorizedAccessException("Task not found or access denied");

            return true;
        }
    }
}