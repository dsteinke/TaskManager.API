using TaskManager.API.DTOs.User;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<User?> GetUserById(Guid userId);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUsername(string username);
        Task<List<User>> GetAllUsers();
        Task<int> UpdateUser(Guid userId, string? username, string? email);
        Task<int> DeleteUser(Guid userId);

    }
}
