using TaskManager.Domain.Enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsCompleted { get; set; }

        public PriorityLevel? Priority { get; set; }
    }
}
