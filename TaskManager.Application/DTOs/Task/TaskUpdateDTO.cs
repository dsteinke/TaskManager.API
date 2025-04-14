using TaskManager.Domain.Enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityLevel? Priority { get; set; }
        public bool? IsCompleted { get; set; } = false;
    }
}
