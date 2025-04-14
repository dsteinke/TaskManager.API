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

            return Ok(new { message = "User registered successfully." });
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
            var result = await _userService.GetUserById(userId);

            return Ok(result);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            var result = await _userService.GetUserByUsername(username);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();

            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            await _userService.DeleteUser();

            return Ok(new { message = "User was deleted successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser
            ([FromBody] UserUpdateDTO userUpdateDTO)
        {
            await _userService.UpdateUser(userUpdateDTO);

            return Ok(new { message = "User was updated successfully" });
        }
    }
}
