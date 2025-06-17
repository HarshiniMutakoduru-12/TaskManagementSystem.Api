using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Common.ResponseModel;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [Route("AddProject")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<string>))]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequestDto project)
        {            
            var result = await _projectService.AddProjectAsync(project);
            return Ok(new GenericResponses<string>().SuccessResponse(result));
        }


        // Get All Projects
        [HttpGet]
        [Route("GetAllProjects")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<List<AddProjectResponseDto>>))]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(new GenericResponses<List<AddProjectResponseDto>>().SuccessResponse(projects));
        }

    }
}
