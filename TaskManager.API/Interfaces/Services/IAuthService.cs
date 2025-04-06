using TaskManager.API.DTOs.User;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO loginDTO);
    }
}
