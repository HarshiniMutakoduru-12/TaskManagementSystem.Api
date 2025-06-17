using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Database;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repos.IRepository;

namespace TaskManagementSystem.Data.Repos.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context, bool isMoc = false) : base(context, isMoc)
        {
        }

    }
    
}
