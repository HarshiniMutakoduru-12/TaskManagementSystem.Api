﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Common.DTOs.ResponseDTO
{
    public class GetTaskByProjectIdResponse 
    {
        public int ProjectId { get; set; }
        public List<AddTaskResponseDto> Tasks { get; set; }
    }
}
