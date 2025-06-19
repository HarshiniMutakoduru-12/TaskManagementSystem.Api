using System.Net.Http.Json;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Common.ResponseModel;
using static System.Net.WebRequestMethods;

namespace TaskManagementSystem.UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AddUserResponseDto>> GetAllUsers()
        {
            var response = await _httpClient.GetFromJsonAsync<GenericResponses<List<AddUserResponseDto>>>("api/User/GetAllUsers");
            return response?.Response!;
        }
    }
}
