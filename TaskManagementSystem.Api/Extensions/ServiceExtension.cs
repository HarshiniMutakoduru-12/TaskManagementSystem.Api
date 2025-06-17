using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Common.Constants;
using TaskManagementSystem.Common.ResponseModel;
using TaskManagementSystem.Data.Database;
using TaskManagementSystem.Data.Repos.IRepository;
using TaskManagementSystem.Data.Repos.Repository;

namespace TaskManagementSystem.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration config)
        {

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoTaskService, ToDoTaskService>();
            services.AddScoped<IProjectService, ProjectService>();



            #endregion

            return services;

        }
    }
}
