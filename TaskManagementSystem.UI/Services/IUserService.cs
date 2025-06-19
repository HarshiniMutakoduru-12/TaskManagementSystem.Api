using TaskManagementSystem.Common.DTOs.ResponseDTO;

namespace TaskManagementSystem.UI.Services
{
    public interface IUserService
    {
        Task<List<AddUserResponseDto>> GetAllUsers();
    }
}
