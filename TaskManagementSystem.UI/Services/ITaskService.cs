using TaskManagementSystem.UI.Models;

namespace TaskManagementSystem.UI.Services
{
    public interface ITaskService
    {
        Task<string> AddTaskAsync(AddTaskRequestDto dto);
        Task<string> UpdateTaskAsync(int taskId, UpdateTaskRequestDto dto);
        Task<UserCompletedTaskCountRespDto> GetTaskCountsAsync();
        Task<TaskAssignedToUserResponseDto> GetTasksAssignedToUserAsync(int userId);
        Task<GenericResponses<List<AddTaskResponseDto>>> SearchTasksAsync(TaskFilterRequestDto filter);
        Task<AddTaskRequestDto?> GetTaskByIdAsync(int taskId);
        Task<string> DeleteTaskAsync(int taskId);
        // Add other endpoint methods as needed
    }

}
