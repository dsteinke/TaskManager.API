﻿namespace TaskManager.API.DTOs.User
{
    public class UserCreateDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
