﻿namespace TaskManagementSystem.UI.Models
{
    public class UpdateTaskRequestDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public bool? IsCompleted { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
    }

}
