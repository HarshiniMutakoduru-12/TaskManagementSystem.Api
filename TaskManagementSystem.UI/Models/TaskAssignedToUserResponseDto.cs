namespace TaskManagementSystem.UI.Models
{
    public class TaskAssignedToUserResponseDto
    {
        public int UserId { get; set; }
        public List<AddTaskResponseDto> Tasks { get; set; } = new();
    }

}
