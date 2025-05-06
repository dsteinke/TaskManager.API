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

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="userCreateDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userCreateDTO)
        {
            await _userService.CreateUser(userCreateDTO);

            return Ok(new { message = "User registered successfully." });
        }

        /// <summary>
        /// Gets user by Email Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            var result = await _userService.GetUserByEmail(email);

            return Ok(result);
        }

        /// <summary>
        /// Gets user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            var result = await _userService.GetUserById(userId);

            return Ok(result);
        }

        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            var result = await _userService.GetUserByUsername(username);

            return Ok(result);
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();

            return Ok(result);
        }

        /// <summary>
        /// Deletes signed in user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            await _userService.DeleteUser();

            return Ok(new { message = "User was deleted successfully" });
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="userUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser
            ([FromBody] UserUpdateDTO userUpdateDTO)
        {
            await _userService.UpdateUser(userUpdateDTO);

            return Ok(new { message = "User was updated successfully" });
        }
    }
}
