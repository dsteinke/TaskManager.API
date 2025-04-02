using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs.User;
using TaskManager.API.Interfaces.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userCreateDTO)
        {
            await _userService.CreateUser(userCreateDTO);

            return Ok();
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            var result = await _userService.GetUserByEmail(email);

            return Ok(result);
        }

        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            await _userService.GetUserById(userId);

            return Ok();
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            await _userService.GetUserByName(username);

            return Ok();
        }
    }
}
