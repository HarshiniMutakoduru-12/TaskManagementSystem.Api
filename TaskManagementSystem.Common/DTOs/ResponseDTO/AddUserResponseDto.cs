using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;

namespace TaskManagementSystem.Common.DTOs.ResponseDTO
{
    public class AddUserResponseDto : AddUserRequestDto
    {
        public int UserId { get; set; }
    }
}
