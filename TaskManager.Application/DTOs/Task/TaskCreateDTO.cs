using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.DTOs.Task
{
    public class TaskCreateDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string? CategoryId { get; set; }
        public string? PriorityId { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
