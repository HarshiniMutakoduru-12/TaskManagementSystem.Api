namespace TaskManagementSystem.UI.Models
{
    public class GetTaskByProjectIdResponse
    {
        public int ProjectId { get; set; }
        public List<AddTaskResponseDto> Tasks { get; set; } = new();
    }

}
