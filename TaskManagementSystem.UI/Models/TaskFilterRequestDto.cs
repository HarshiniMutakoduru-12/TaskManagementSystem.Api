namespace TaskManagementSystem.UI.Models
{
    public class TaskFilterRequestDto
    {
        public int? UserId { get; set; }
        public bool? IsCompleted { get; set; }
        public int? ProjectId { get; set; }
        public string? TitleFilter { get; set; }
        public DateTime? DueDateFilter { get; set; }
        public string SortColumn { get; set; } = "TaskId";
        public bool IsAscending { get; set; } = true;
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
