using TaskManager.Domain.Enums;

namespace TaskManager.API.Models
{
    public class Task
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool IsCompleted { get; set; }

    }
}
