using TaskManager.API.DTOs.User;

namespace TaskManager.API.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateDTO userDTO);
        Task<UserDTO> GetUserById(Guid userId);
        Task<UserDTO> GetUserByUsername(string username);
        Task<UserDTO> GetUserByEmail(string email);
        Task<List<UserDTO>> GetAllUsers();
        Task<bool> UpdateUser(Guid userId, UserUpdateDTO userDTO);
        Task<bool> DeleteUser(Guid userId);
    }
}
