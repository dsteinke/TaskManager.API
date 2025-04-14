using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManager.API.DTOs.User;
using TaskManager.API.Interfaces.Repositories;
using TaskManager.API.Interfaces.Services;
using TaskManager.API.Models;

namespace TaskManager.API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService
            (IUserRepository userRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
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

        public Guid GetUserId_LoggedInUser()
        {
            var userId = 
                _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(userId, out var guid) ? guid : throw new UnauthorizedAccessException("Invalid token");
        }

        public async Task<bool> DeleteUser()
        {
            var userId = GetUserId_LoggedInUser();

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

        public async Task<bool> UpdateUser(UserUpdateDTO userDTO)
        {
            var userId = GetUserId_LoggedInUser();

            await _userRepository.UpdateUser(userId, userDTO.Username, userDTO.Email);

            return true;
        }
    }
}
