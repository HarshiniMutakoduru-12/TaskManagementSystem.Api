namespace TaskManagementSystem.UI.Models
{
    public class AddTaskRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Priority { get; set; } = "Low"; // Default value
        public bool IsCompleted { get; set; } = false;
        public int UserId { get; set; }
        public int? ProjectId { get; set; }
    }

}
