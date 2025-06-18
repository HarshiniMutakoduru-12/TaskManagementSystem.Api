using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Application.IServices
{
    public interface IToDoTaskService
    {
        Task<string> AddTaskAsync(AddTaskRequestDto project);
        Task<List<AddTaskResponseDto>> GetAllTasksAsync();
        Task<TaskAssignedToUserResponseDto> GetTasksAssignedToUserAsync(int userId);
        Task<List<TaskAssignedToUserResponseDto>> GetOverdueOrIncompleteTasksAsync();
        Task<UserCompletedTaskCountRespDto> GetCompletedTaskCountByUserAsync(int userId);
        Task<List<AddTaskResponseDto>> GetTasksDueThisWeekAsync();
        Task<GetTaskByProjectIdResponse> GetTaskByProjectIdAsync(int projectId);
        Task<List<TaskAssignedToUserResponseDto>> GetOverdueOrIncompleteTasksByUserIdAsync(int userId);


    }
}
