using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Repos.IRepository
{
    public interface IToDoTaskRepository : IGenericRepository<ToDoTask>
    {
        Task<List<ToDoTask>> GetTasksDueThisWeekAsync();
        Task<List<TaskPaginatedRespDto>> GetPaginatedTaskList(TaskFilterRequestDto filter);

    }
}
