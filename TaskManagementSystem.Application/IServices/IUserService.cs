using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;

namespace TaskManagementSystem.Application.IServices
{
    public interface IUserService
    {
        Task<string> AddUserAsync(AddUserRequestDto user);
        Task<List<AddUserResponseDto>> GetAllUsersAsync();


    }
}
