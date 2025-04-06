﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
