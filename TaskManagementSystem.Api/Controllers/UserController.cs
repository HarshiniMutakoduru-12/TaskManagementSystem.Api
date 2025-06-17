using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Common.ResponseModel;

namespace TaskManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("AddUser")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<string>))]

        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto user)
        {           
            var result = await _userService.AddUserAsync(user);
            return Ok(new GenericResponses<string>().SuccessResponse(result));
        }
        [HttpGet]
        [Route("GetAllUsers")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<List<AddUserResponseDto>>))]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(new GenericResponses<List<AddUserResponseDto>>().SuccessResponse(users));
        }
    }
}
