using TaskManagementSystem.Common.DTOs.ResponseDTO;

namespace TaskManagementSystem.UI.Services
{
    public interface IProjectService
    {
        Task<List<AddProjectResponseDto>> GetAllProjects();
    }
}
