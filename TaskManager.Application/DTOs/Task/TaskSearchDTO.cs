using TaskManager.Domain.Enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskSearchDTO
    {
        public string? Title { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public PriorityLevel? Priority { get; set; }
        public string? SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}
