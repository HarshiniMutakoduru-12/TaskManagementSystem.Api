using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.UI.Models
{
    public class AddTaskRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; } = "Low"; // Default value
        public bool IsCompleted { get; set; } = false;
        public int UserId { get; set; }
        public int? ProjectId { get; set; }
    }
    public class TaskModal
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public string Priority { get; set; } = "Low"; // Default value
        [Required]
        public bool IsCompleted { get; set; } = false;
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? ProjectId { get; set; }
    }

}
