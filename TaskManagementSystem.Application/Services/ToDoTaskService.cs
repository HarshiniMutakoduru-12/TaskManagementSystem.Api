using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Common.Constants;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Common.Exceptions;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repos.IRepository;

namespace TaskManagementSystem.Application.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        IMapper _mapper;
        IToDoTaskRepository _toDoTaskRepository;
        IUserRepository _userRepository;
        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository,IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _toDoTaskRepository = toDoTaskRepository;
            _userRepository = userRepository;
        }

        // Add ToDo Task
        public async Task<string> AddTaskAsync(AddTaskRequestDto project)
        {
            var userId = await _userRepository.GetByIdAsync(project.UserId) ?? throw new AppException(ApiErrorCodeMessages.UserNotFound);
            if (project.ProjectId.HasValue)
            {
               var projectId = await _toDoTaskRepository.GetByIdAsync(project.ProjectId.Value);
                if (projectId == null)
                {
                    throw new Exception(ApiErrorCodeMessages.ProjectNotFound); 
                }
            }
            var projects = _mapper.Map<ToDoTask>(project);
            projects.CreatedOn = DateTime.UtcNow;
            projects.CreatedBy = "1";
            await _toDoTaskRepository.AddAsync(projects);
            return ApiErrorCodeMessages.TaskAddedSuccessfully;
        }

        // Get All ToDo Tasks
        public async Task<List<AddTaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _toDoTaskRepository.GetAll().ToListAsync();
            return _mapper.Map<List<AddTaskResponseDto>>(tasks);
        }
        public async Task<TaskAssignedToUserResponseDto> GetTasksAssignedToUserAsync(int userId)
        {

            var user = await _userRepository.GetByIdAsync(userId) ?? throw new AppException(ApiErrorCodeMessages.UserNotFound);
            var tasks = await _toDoTaskRepository.GetAll()
            .Where(task => task.UserId == userId) 
            .ToListAsync();


            var userTasks = new TaskAssignedToUserResponseDto
            {
                UserId = userId,
                Tasks = _mapper.Map<List<AddTaskResponseDto>>(tasks)
            };

            return userTasks;
        }


        public async Task<List<TaskAssignedToUserResponseDto>> GetOverdueOrIncompleteTasksAsync()
        {           
            var currentDate = DateTime.Now;         
            var tasks = await _toDoTaskRepository.GetAll()
                .Where(task => (task.DueDate < currentDate && !task.IsCompleted) 
                            || (!task.IsCompleted)) 
                .ToListAsync();
          var allTasks =  _mapper.Map<List<AddTaskResponseDto>>(tasks);

            var groupedTasks = allTasks
                .GroupBy(task => task.UserId)  
                .Select(userGroup => new TaskAssignedToUserResponseDto
                {
                    UserId = userGroup.Key, 
                    Tasks = userGroup.ToList()  
                })
                .OrderBy(u => u.UserId) 
                .ToList();  

            return groupedTasks;
        }



        public async Task<UserCompletedTaskCountRespDto> GetCompletedTaskCountByUserAsync(int userId)
        {    
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new AppException(ApiErrorCodeMessages.UserNotFound);
            var tasks = await _toDoTaskRepository.GetAll()
                .Where(task => task.IsCompleted && task.UserId == userId)  
                .ToListAsync();          


            var completedTaskCounts = new UserCompletedTaskCountRespDto { UserId = userId, CompletedTaskCount= tasks.Count};

            return completedTaskCounts;
        }



        public async Task<List<AddTaskResponseDto>> GetTasksDueThisWeekAsync()
        {
            // Get the current date
           
            var tasks = await _toDoTaskRepository.GetTasksDueThisWeekAsync();
            var allTasks =  _mapper.Map<List<AddTaskResponseDto>>(tasks);

            return allTasks;
        }



    }

}
