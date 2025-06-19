using System.Net.Http.Json;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Common.ResponseModel;

namespace TaskManagementSystem.UI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AddProjectResponseDto>> GetAllProjects()
        {
            var response = await _httpClient.GetFromJsonAsync<GenericResponses<List<AddProjectResponseDto>>>("api/Project/GetAllProjects");
            return response?.Response!;
        }
    }
}
