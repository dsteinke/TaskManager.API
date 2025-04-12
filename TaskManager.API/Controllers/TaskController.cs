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

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDTO taskCreateDTO)
        {
            await _taskService.CreateTask(taskCreateDTO);

            return Ok(new { message = "Task successfully created" });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasksForUser()
        {
            var result = await _taskService.GetAllTasksForUser();

            return Ok(result);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid taskId)
        {
            var result = await _taskService.GetTaskById(taskId);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTasks([FromQuery] TaskSearchDTO taskSearchDTO)
        {
            var result = await _taskService.SearchTask(taskSearchDTO);

            return Ok(result);
        }


        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTask
            ([FromRoute] Guid taskId, [FromBody] TaskUpdateDTO taskUpdateDTO)
        {
            await _taskService.UpdateTask(taskId, taskUpdateDTO);

            return Ok(new { message = "Task successfully updated" });
        }


        [HttpDelete("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            await _taskService.DeleteTask(taskId);

            return Ok(new { message = "Task successfully deleted" });
        }
    }
}
