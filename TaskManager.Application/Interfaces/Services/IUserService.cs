using TaskManager.API.DTOs.User;

namespace TaskManager.API.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateDTO userDTO);
        Guid GetUserId_LoggedInUser();
        Task<UserDTO> GetUserById(Guid userId);
        Task<UserDTO> GetUserByUsername(string username);
        Task<UserDTO> GetUserByEmail(string email);
        Task<List<UserDTO>> GetAllUsers();
        Task<bool> UpdateUser(UserUpdateDTO userDTO);
        Task<bool> DeleteUser();
    }
}
