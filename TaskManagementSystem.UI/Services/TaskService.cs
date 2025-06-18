using System.Net.Http.Json;
using TaskManagementSystem.UI.Models;

namespace TaskManagementSystem.UI.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _http;

        public TaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<GenericResponses<List<AddTaskResponseDto>>> SearchTasksAsync(TaskFilterRequestDto filter)
        {
            var response = await _http.PostAsJsonAsync("api/ToDoTask/SearchTasks", filter);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GenericResponses<List<AddTaskResponseDto>>>();
                return result ?? new GenericResponses<List<AddTaskResponseDto>>();
            }

            return new GenericResponses<List<AddTaskResponseDto>>()
                .ErrorResponse("Failed to fetch tasks from server.");
        }


        public async Task<string> AddTaskAsync(AddTaskRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/ToDoTask/AddToDoTask", dto);
            var result = await response.Content.ReadFromJsonAsync<GenericResponses<string>>();
            return result?.Response!;
        }

        public async Task<string> UpdateTaskAsync(int taskId, UpdateTaskRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/ToDoTask/UpdateTask/{taskId}", dto);
            var result = await response.Content.ReadFromJsonAsync<GenericResponses<string>>();
            return result?.Response!;
        }

        public async Task<UserCompletedTaskCountRespDto> GetTaskCountsAsync()
        {
            var response = await _http.GetFromJsonAsync<GenericResponses<UserCompletedTaskCountRespDto>>("api/ToDoTask/GetTaskCounts");
            return response?.Response!;
        }

        public async Task<TaskAssignedToUserResponseDto> GetTasksAssignedToUserAsync(int userId)
        {
            var response = await _http.GetFromJsonAsync<GenericResponses<TaskAssignedToUserResponseDto>>($"api/ToDoTask/GetTasksAssignedToUser/{userId}");
            return response?.Response!;
        }

        public async Task<AddTaskRequestDto?> GetTaskByIdAsync(int taskId)
        {
            var response = await _http.GetFromJsonAsync<GenericResponses<AddTaskRequestDto>>($"api/ToDoTask/GetTaskById/{taskId}");
            return response?.Response;
        }

        public async Task<string> DeleteTaskAsync(int taskId)
        {
            var response = await _http.DeleteAsync($"api/ToDoTask/DeleteTask/{taskId}");

            if (!response.IsSuccessStatusCode)
                return "Error in deleting task";

            var result = await response.Content.ReadFromJsonAsync<GenericResponses<string>>();
            return result?.Response;
        }
    }

}
