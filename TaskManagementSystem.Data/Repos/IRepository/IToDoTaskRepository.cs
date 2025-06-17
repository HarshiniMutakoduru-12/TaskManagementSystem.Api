using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Repos.IRepository
{
    public interface IToDoTaskRepository : IGenericRepository<ToDoTask>
    {
        Task<List<ToDoTask>> GetTasksDueThisWeekAsync();

    }
}
