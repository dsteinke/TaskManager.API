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

            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasksForUser()
        {
            var result = await _taskService.GetAllTasksForUser();

            return Ok(result);
        }
    }
}
