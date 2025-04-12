using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.API.DTOs.User
{
    public class UserCreateDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("Password")]
        public string PasswordHash { get; set; }
        [Required]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }
    }
}
