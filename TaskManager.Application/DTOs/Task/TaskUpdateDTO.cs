namespace TaskManager.API.DTOs.Task
{
    public class TaskUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string? PriorityId { get; set; }
        public bool? IsCompleted { get; set; } = false;
    }
}
