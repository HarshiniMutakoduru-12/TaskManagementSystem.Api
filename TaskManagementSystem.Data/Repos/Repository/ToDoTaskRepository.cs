
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Data.Database;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repos.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagementSystem.Data.Repos.Repository
{
    public class ToDoTaskRepository : GenericRepository<ToDoTask>, IToDoTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoTaskRepository(ApplicationDbContext context, bool isMoc = false) : base(context, isMoc)
        {
            _context = context;
        }
        public async Task<List<ToDoTask>> GetTasksDueThisWeekAsync()
        {
            var currentDate = DateTime.Now; // Get the current date in UTC
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
            
            DateTime endOfWeek = startOfWeek.AddDays(6);  // Sunday is 6 days after Monday
            var query = @"
                SELECT * 
                FROM ToDoTasks
                WHERE cast(DueDate as date) >= @StartOfWeek  AND cast(DueDate as date) <= @EndOfWeek  and isCompleted = 0";
            
            var tasks =await _context.ToDoTasks.FromSqlRaw(query, new SqlParameter("@StartOfWeek", startOfWeek), new SqlParameter("@EndOfWeek", endOfWeek)).ToListAsync();

            return tasks;
        }

        public async Task<List<TaskPaginatedRespDto>> GetPaginatedTaskList(TaskFilterRequestDto filter)
        {
            var query = GetAll();
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

           

            // Total count of tasks that match the filters
            var totalCount = await query.CountAsync();

            // Paginate the results
            var tasks = await query.Include(x => x.User).Include(x => x.Project)
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
                    TotalCount = totalCount,
                    ProjectName = x.Project != null ? x.Project.Name : "",
                    UserName = x.User != null ? x.User.Name : "",
                })
                .ToListAsync();

            // Calculate row number for pagination
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].RowNumber = i + (filter.CurrentPage - 1) * filter.PageSize + 1;
            }

            return tasks;
        }

    }
}
