using AutoMapper;
using TaskManager.API.DTOs.User;
using TaskManager.API.Interfaces.Repositories;
using TaskManager.API.Interfaces.Services;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UserCreateDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserByEmail(userDTO.Email);

            if (existingUser != null)
                throw new Exception($"User with Email: {userDTO.Email} already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);

            var user = _mapper.Map<User>(userDTO);
            user.PasswordHash = hashedPassword;

            await _userRepository.CreateUser(user);

            return true;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var existingUser = await _userRepository.GetUserById(userId);

            if (existingUser == null)
                throw new KeyNotFoundException($"User with userId: {userId} does not exist");

            await _userRepository.DeleteUser(userId);

            return true;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
                throw new KeyNotFoundException($"No user with Email: {email} found");

            var result = _mapper.Map<UserDTO>(user);

            return result;
        }

        public async Task<UserDTO> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException($"No user with userId: {userId} found");

            var result = _mapper.Map<UserDTO>(user);

            return result;
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
                throw new KeyNotFoundException($"No user with username: {username} found");

            var result = _mapper.Map<UserDTO>(user);

            return result;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var allUsers = await _userRepository.GetAllUsers();

            var result = _mapper.Map<List<UserDTO>>(allUsers);

            return result;
        }

        public async Task<bool> UpdateUser(Guid userId, UserUpdateDTO userDTO)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException($"No user with userId: {userId} found");

            await _userRepository.UpdateUser(userId, userDTO.Username, userDTO.Email);

            return true;
        }
    }
}
