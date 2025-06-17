
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Get the end of the current week (Sunday)
            DateTime endOfWeek = startOfWeek.AddDays(6);  // Sunday is 6 days after Monday
            var query = @"
                SELECT * 
        FROM ToDoTasks
      WHERE cast(DueDate as date) >= @StartOfWeek  AND cast(DueDate as date) <= @EndOfWeek  and isCompleted = 0";

            // Execute the raw SQL query using FromSqlRaw with parameters
            var tasks =await _context.ToDoTasks.FromSqlRaw(query, new SqlParameter("@StartOfWeek", startOfWeek), new SqlParameter("@EndOfWeek", endOfWeek)).ToListAsync();

            return tasks;
        }

    }
}
