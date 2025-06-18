using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Common.ResponseModel;
using TaskManagementSystem.Data.Models;


namespace TaskManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskService _toDoTaskService;
        public ToDoTaskController(IToDoTaskService toDoTaskService)
        {
            _toDoTaskService = toDoTaskService;
        }

        [HttpPost]
        [Route("AddToDoTask")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<string>))]

        public async Task<IActionResult> AddTask([FromBody] AddTaskRequestDto toDoTask)
        {
            var result = await _toDoTaskService.AddTaskAsync(toDoTask);
            return Ok(new GenericResponses<string>().SuccessResponse(result));
        }
        [HttpPost]
        [Route("SearchTasks")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<List<TaskPaginatedRespDto>>))]
        public async Task<IActionResult> SearchTasks([FromBody] TaskFilterRequestDto filter)
        {
            var tasks = await _toDoTaskService.GetAllTasksAsync(filter);

            return Ok(new GenericResponses<List<TaskPaginatedRespDto>>().SuccessResponse(tasks));
        }
        [HttpGet]
        [Route("GetTasksAssignedToUser/{userId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<TaskAssignedToUserResponseDto>))]

        public async Task<IActionResult> GetTasksAssignedToUser(int userId)
        {
            var tasks = await _toDoTaskService.GetTasksAssignedToUserAsync(userId);
            return Ok(new GenericResponses<TaskAssignedToUserResponseDto>().SuccessResponse(tasks));
        }
        [HttpGet]
        [Route("GetOverdueOrIncompleteTasks")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<List<TaskAssignedToUserResponseDto>>))]
        public async Task<IActionResult> GetOverdueOrIncompleteTasks()
        {

            var tasks = await _toDoTaskService.GetOverdueOrIncompleteTasksAsync();
            return Ok(new GenericResponses<List<TaskAssignedToUserResponseDto>>().SuccessResponse(tasks));
        }

        ////GetCompletedTaskCountByUserAsync
        //[HttpGet]
        //[Route("GetCompletedTaskCountByUser/{userId}")]
        //[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<UserCompletedTaskCountRespDto>))]
        //public async Task<IActionResult> GetCompletedTaskCountByUser(int userId)
        //{
        //    var count = await _toDoTaskService.GetCompletedTaskCountByUserAsync(userId);
        //    return Ok(new GenericResponses<UserCompletedTaskCountRespDto>().SuccessResponse(count));
        //}

        [HttpGet]

        [Route("GetTasksDueThisWeek")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<List<AddTaskResponseDto>>))]
        public async Task<IActionResult> GetTasksDueThisWeek()
        {
            var tasks = await _toDoTaskService.GetTasksDueThisWeekAsync();
            return Ok(new GenericResponses<List<AddTaskResponseDto>>().SuccessResponse(tasks));
        }
        //[HttpGet]
        //[Route("GetTaskByProjectId/{projectId}")]
        //[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<GetTaskByProjectIdResponse>))]
        //public async Task<IActionResult> GetTaskByProjectId(int projectId)
        //{
        //    var tasks = await _toDoTaskService.GetTaskByProjectIdAsync(projectId);
        //    return Ok(new GenericResponses<GetTaskByProjectIdResponse>().SuccessResponse(tasks));
        //}
        [HttpGet]
        [Route("GetOverdueOrIncompleteTasksByUserId/{userId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<List<TaskAssignedToUserResponseDto>>))]
        public async Task<IActionResult> GetOverdueOrIncompleteTasksByUserId(int userId)
        {
            var tasks = await _toDoTaskService.GetOverdueOrIncompleteTasksByUserIdAsync(userId);
            return Ok(new GenericResponses<List<TaskAssignedToUserResponseDto>>().SuccessResponse(tasks));
        }
        [HttpGet]
        [Route("GetTaskCounts")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<UserCompletedTaskCountRespDto>))]
        public async Task<IActionResult> GetTaskCounts()
        {
            var taskCounts = await _toDoTaskService.GetTaskCountsAsync();
            return Ok(new GenericResponses<UserCompletedTaskCountRespDto>().SuccessResponse(taskCounts));
        }
        [HttpPut]
        [Route("UpdateTask/{taskId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<string>))]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskRequestDto taskRequest)
        {
            var result = await _toDoTaskService.UpdateTaskAsync(taskId, taskRequest);
            return Ok(new GenericResponses<string>().SuccessResponse(result));
        }
        [HttpGet]
        [Route("GetTaskById/{taskId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<AddTaskRequestDto>))]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var task = await _toDoTaskService.GetTaskByIdAsync(taskId);
            return Ok(new GenericResponses<AddTaskRequestDto>().SuccessResponse(task));
        }
        [HttpDelete]
        [Route("DeleteTask/{taskId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericResponses<string>))]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var result = await _toDoTaskService.DeleteTaskAsync(taskId);
            return Ok(new GenericResponses<string>().SuccessResponse(result));
        }
    }
}
