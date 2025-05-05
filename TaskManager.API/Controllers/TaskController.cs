using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Creates new task
        /// </summary>
        /// <param name="taskCreateDTO"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDTO taskCreateDTO)
        {
            await _taskService.CreateTask(taskCreateDTO);

            return Ok(new { message = "Task successfully created" });
        }

        /// <summary>
        /// Gets all tasks of signed in user
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasksForUser()
        {
            var result = await _taskService.GetAllTasksForUser();

            return Ok(result);
        }

        /// <summary>
        /// Gets task by taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid taskId)
        {
            var result = await _taskService.GetTaskById(taskId);

            return Ok(result);
        }

        /// <summary>
        /// Searches task of signed in user
        /// </summary>
        /// <param name="taskSearchDTO"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchTasks([FromQuery] TaskSearchDTO taskSearchDTO)
        {
            var result = await _taskService.SearchTask(taskSearchDTO);

            return Ok(result);
        }

        /// <summary>
        /// Updates task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTask
            ([FromRoute] Guid taskId, [FromBody] TaskUpdateDTO taskUpdateDTO)
        {
            await _taskService.UpdateTask(taskId, taskUpdateDTO);

            return Ok(new { message = "Task successfully updated" });
        }

        /// <summary>
        /// Deletes task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            await _taskService.DeleteTask(taskId);

            return Ok(new { message = "Task successfully deleted" });
        }
    }
}
