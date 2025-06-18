using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Common.DTOs.ResponseDTO
{
    public class TaskPaginatedRespDto : AddTaskResponseDto
    {
        public int TotalCount { get; set; }
        public int RowNumber { get; set; }
    }
}
