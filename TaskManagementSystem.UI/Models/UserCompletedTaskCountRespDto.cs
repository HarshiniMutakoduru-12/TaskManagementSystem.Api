namespace TaskManagementSystem.UI.Models
{
    public class UserCompletedTaskCountRespDto
    {
        public int TotalTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public int OverdueTaskCount { get; set; }
    }

}
