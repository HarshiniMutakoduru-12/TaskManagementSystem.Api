using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;

namespace TaskManagementSystem.Application.IServices
{
    public interface IProjectService
    {
        Task<string> AddProjectAsync(AddProjectRequestDto project);
        Task<List<AddProjectResponseDto>> GetAllProjectsAsync();


    }
}
