using TaskManager.API.DTOs.User;

namespace TaskManager.API.DTOs.Task
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CategoryId { get; set; }
        public string PriorityId { get; set; }
        public bool IsCompleted { get; set; }

        public UserDTO User { get; set; }
        public CategoryDTO Category { get; set; }
        public PriorityDTO Priority { get; set; }
    }
}
