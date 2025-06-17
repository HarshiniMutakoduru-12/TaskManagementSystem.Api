using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Common.DTOs.ResponseDTO
{
    public class UserCompletedTaskCountRespDto
    {
        public int UserId { get; set; } 
        public int CompletedTaskCount { get; set; }
    }
}
