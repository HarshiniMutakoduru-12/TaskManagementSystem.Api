namespace TaskManagementSystem.UI.Models
{
    public class AddTaskResponseDto : AddTaskRequestDto
    {
        public int TaskId { get; set; }
        public int TotalCount { get; set; }
        public int RowNumber { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
    }

}
