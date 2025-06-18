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
        IProjectRepository _projectRepository;
        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository,IMapper mapper, IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _toDoTaskRepository = toDoTaskRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
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
        // Update ToDo Task
        public async Task<string> UpdateTaskAsync(int taskId, UpdateTaskRequestDto taskRequest)
        {
            var task = await _toDoTaskRepository.GetByIdAsync(taskId) ?? throw new AppException(ApiErrorCodeMessages.TaskNotFound);
            task.UpdatedOn = DateTime.UtcNow;
            task.UpdatedBy = "1";
            task.UserId = taskRequest.UserId ?? task.UserId;
            task.ProjectId = taskRequest.ProjectId ?? task.ProjectId;
            task.Title = taskRequest.Title ?? task.Title;
            task.Description = taskRequest.Description ?? task.Description;
            task.DueDate = taskRequest.DueDate ?? task.DueDate;
            task.Priority = taskRequest.Priority ?? task.Priority;
            task.IsCompleted = taskRequest.IsCompleted ?? task.IsCompleted;           
            task.UpdatedOn = DateTime.UtcNow;
            await _toDoTaskRepository.UpdateAsync(task);
            return ApiErrorCodeMessages.TaskUpdatedSuccessfully;
        }

        // Get All ToDo Tasks
        public async Task<List<TaskPaginatedRespDto>> GetAllTasksAsync(TaskFilterRequestDto filter)
        {
            var query =  _toDoTaskRepository.GetAll();
            if (filter.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == filter.UserId.Value);
            }

            if (filter.IsCompleted.HasValue)
            {
                query = query.Where(x => x.IsCompleted == filter.IsCompleted.Value);
            }

            if (filter.ProjectId.HasValue)
            {
                query = query.Where(x => x.ProjectId == filter.ProjectId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.TitleFilter))
            {
                query = query.Where(x => x.Title.ToLower().Contains(filter.TitleFilter.ToLower()));
            }

            if (filter.DueDateFilter.HasValue)
            {
                query = query.Where(x => x.DueDate.Date == filter.DueDateFilter.Value.Date);
            }

            // Apply sorting based on the provided SortColumn and IsAscending
            switch (filter.SortColumn)
            {
                case "Title":
                    query = filter.IsAscending
                        ? query.OrderBy(x => x.Title.ToLower())
                        : query.OrderByDescending(x => x.Title.ToLower());
                    break;
                case "DueDate":
                    query = filter.IsAscending
                        ? query.OrderBy(x => x.DueDate)
                        : query.OrderByDescending(x => x.DueDate);
                    break;
                case "Priority":
                    query = filter.IsAscending
                        ? query.OrderBy(x => x.Priority)
                        : query.OrderByDescending(x => x.Priority);
                    break;
                default:
                    query = query.OrderBy(x => x.TaskId); // Default sort by TaskId
                    break;
            }

            // Total count of tasks that match the filters
            var totalCount = await query.CountAsync();

            // Paginate the results
            var tasks = await query
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new TaskPaginatedRespDto
                {
                    TaskId = x.TaskId,
                    Title = x.Title,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    Priority = x.Priority,
                    IsCompleted = x.IsCompleted,
                    UserId = x.UserId,
                    ProjectId = x.ProjectId,
                    TotalCount = totalCount
                })
                .ToListAsync();

            // Calculate row number for pagination
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].RowNumber = i + (filter.CurrentPage - 1) * filter.PageSize + 1;
            }

            return tasks;
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



        public async Task<List<TaskAssignedToUserResponseDto>> GetOverdueOrIncompleteTasksByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new AppException(ApiErrorCodeMessages.UserNotFound);
            var currentDate = DateTime.Now;
            var tasks = await _toDoTaskRepository.GetAll()
                .Where(task => task.UserId == userId && ((task.DueDate < currentDate && !task.IsCompleted)
                            || (!task.IsCompleted)))
                .ToListAsync();
            var allTasks = _mapper.Map<List<AddTaskResponseDto>>(tasks);

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

      


        public async Task<List<AddTaskResponseDto>> GetTasksDueThisWeekAsync()
        {
            // Get the current date
           
            var tasks = await _toDoTaskRepository.GetTasksDueThisWeekAsync();
            var allTasks =  _mapper.Map<List<AddTaskResponseDto>>(tasks);

            return allTasks;
        }
        public async Task<UserCompletedTaskCountRespDto> GetTaskCountsAsync()
        {
                       
            var tasks = await _toDoTaskRepository.GetAll()               
                .ToListAsync();

         
            var totalTaskCount = tasks.Count;
            var completedTaskCount = tasks.Count(task => task.IsCompleted);
            var overdueTaskCount = tasks.Count(task => task.DueDate < DateTime.UtcNow && !task.IsCompleted);

            // Create the response DTO
            var taskCounts = new UserCompletedTaskCountRespDto
            {                
                TotalTaskCount = totalTaskCount,
                CompletedTaskCount = completedTaskCount,
                OverdueTaskCount = overdueTaskCount
            };

            return taskCounts;
        }
        public async Task<AddTaskRequestDto> GetTaskByIdAsync(int taskId)
        {
            var task = await _toDoTaskRepository.GetByIdAsync(taskId) ?? throw new AppException(ApiErrorCodeMessages.TaskNotFound);
            var taskDto = _mapper.Map<AddTaskRequestDto>(task);
            return taskDto;
        }
        //delete by taskId
        public async Task<string> DeleteTaskAsync(int taskId)
        {
            var task = await _toDoTaskRepository.GetByIdAsync(taskId) ?? throw new AppException(ApiErrorCodeMessages.TaskNotFound);
            await _toDoTaskRepository.DeleteAsync(taskId);
            return ApiErrorCodeMessages.TaskDeletedSuccessfully;
        }


    }

}
