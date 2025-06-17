using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Common.DTOs.RequestDTO
{
    public class AddTaskRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public int? ProjectId { get; set; }
    }
}
