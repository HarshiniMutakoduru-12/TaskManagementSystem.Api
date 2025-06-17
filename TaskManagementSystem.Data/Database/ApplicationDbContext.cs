using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ToDoTask> ToDoTasks { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Seeding Users
        //    modelBuilder.Entity<User>().HasData(
        //        new User
        //        {
                    
        //            Name = "TestUser1",
        //            Email = "TestUser1@gmail.com",
        //            Role = "Developer",
        //            CreatedOn = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new User
        //        {
                  
        //            Name = "TestUser2",
        //            Email = "TestUser2@example.com",
        //            Role = "Manager",
        //            CreatedOn = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new User
        //        {
                    
        //            Name = "TestUser3",
        //            Email = "TestUser3@gmail.com",
        //            Role = "Developer",
        //            CreatedOn = DateTime.UtcNow,
        //            IsDeleted = false
        //        }
        //    );

        //    // Seeding Projects
        //    modelBuilder.Entity<Project>().HasData(
        //        new Project
        //        {                    
        //            Name = "Website Redesign",
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = "1",
        //            UpdatedOn = DateTime.UtcNow,
        //            UpdatedBy = "1"
        //        },
        //        new Project
        //        {
                    
        //            Name = "Mobile App Development",
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = "1",
        //            UpdatedOn = DateTime.UtcNow,
        //            UpdatedBy = "1"
        //        }
        //    );

        //    // Seeding ToDoTasks
        //    modelBuilder.Entity<ToDoTask>().HasData(
        //        new ToDoTask
        //        {
                    
        //            Title = "Design Homepage",
        //            Description = "Create the homepage UI design.",
        //            DueDate = DateTime.UtcNow.AddDays(3),
        //            Priority = "High",
        //            IsCompleted = false,
        //            UserId = 1,  
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = "1",
        //            ProjectId = 1
        //        },
        //        new ToDoTask
        //        {
                   
        //            Title = "Develop Login Feature",
        //            Description = "Implement login functionality for the website.",
        //            DueDate = DateTime.UtcNow.AddDays(5),
        //            Priority = "Medium",
        //            IsCompleted = false,
        //            UserId = 2, 
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = "1",
        //            ProjectId = 1
        //        },
        //        new ToDoTask
        //        {
                   
        //            Title = "Build Admin Dashboard",
        //            Description = "Create an admin dashboard for managing users.",
        //            DueDate = DateTime.UtcNow.AddDays(7),
        //            Priority = "Low",
        //            IsCompleted = false,
        //            UserId = 3,
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = "1",
        //            ProjectId = 2
        //        },
        //        new ToDoTask
        //        {
                   
        //            Title = "Test Mobile App UI",
        //            Description = "Perform UI testing for the mobile app.",
        //            DueDate = DateTime.UtcNow.AddDays(10),
        //            Priority = "High",
        //            IsCompleted = false,
        //            UserId = 2, 
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = "1",
        //            ProjectId = 2
        //        }
        //    );
        //}
    }
    }
