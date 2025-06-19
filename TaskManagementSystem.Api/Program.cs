using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Api.Extensions;
using TaskManagementSystem.Api.Middleware;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Data.Database;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repos.IRepository;
using TaskManagementSystem.Data.Repos.Repository;
using TaskManagementSystem.Infrastructure.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.RegisterAllServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureDeleted();

    dbContext.Database.Migrate();

    SeedData(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();


void SeedData(ApplicationDbContext context)
{
    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Role = "Software Developer", 
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            },
            new User
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Role = "Software Developer",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            },
            new User
            {
                Name = "Mike Johnson",
                Email = "mike.johnson@example.com",
                Role = "Software Developer",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            },
            new User
            {
                Name = "Emma Wilson",
                Email = "emma.wilson@example.com",
                Role = "Software Developer",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            },
            new User
            {
                Name = "Chris Brown",
                Email = "chris.brown@example.com",
                Role = "Software Developer",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            }
        );
        context.SaveChanges();
    }

    if (!context.Projects.Any())
    {
        context.Projects.AddRange(
            new Project
            {
                Name = "Website Redesign",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "1"
            },
            new Project
            {
                Name = "Mobile App Development",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "1"
            },
            new Project
            {
                Name = "E-Commerce Platform",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "1"
            }
        );
        context.SaveChanges();
    }

    if (!context.ToDoTasks.Any())
    {
        context.ToDoTasks.AddRange(
            new ToDoTask
            {
                Title = "Design Homepage",
                Description = "Create the homepage UI design.",
                DueDate = DateTime.UtcNow.AddDays(-1),
                Priority = "High",
                IsCompleted = false,
                UserId = 1,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 1
            },
            new ToDoTask
            {
                Title = "Develop Login Feature",
                Description = "Implement login functionality for the website.",
                DueDate = DateTime.UtcNow.AddDays(5),
                Priority = "Medium",
                IsCompleted = false,
                UserId = 1,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 1
            },
            new ToDoTask
            {
                Title = "Task3",
                Description = "Implement login functionality for the website.",
                DueDate = DateTime.UtcNow.AddDays(-1),
                Priority = "Medium",
                IsCompleted = true,
                UserId = 1,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 1
            },
            new ToDoTask
            {
                Title = "Create Project Dashboard",
                Description = "Develop the project management dashboard.",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = "High",
                IsCompleted = false,
                UserId = 2, 
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 1
            },
            new ToDoTask
            {
                Title = "API Integration",
                Description = "Integrate external APIs for payment gateway.",
                DueDate = DateTime.UtcNow.AddDays(-2),
                Priority = "Low",
                IsCompleted = false,
                UserId = 2,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 1
            },
             new ToDoTask
             {
                 Title = "Task3",
                 Description = "Integrate external APIs for payment gateway.",
                 DueDate = DateTime.UtcNow.AddDays(-1),
                 Priority = "Low",
                 IsCompleted = true,
                 UserId = 2,
                 CreatedOn = DateTime.UtcNow,
                 CreatedBy = "1",
                 ProjectId = 1
             },
            new ToDoTask
            {
                Title = "Set Up Database Schema",
                Description = "Create the initial database schema for the application.",
                DueDate = DateTime.UtcNow.AddDays(4),
                Priority = "Medium",
                IsCompleted = false,
                UserId = 3,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 2
            },
            new ToDoTask
            {
                Title = "Test User Authentication",
                Description = "Perform tests on user login and authentication.",
                DueDate = DateTime.UtcNow.AddDays(6),
                Priority = "High",
                IsCompleted = false,
                UserId = 3,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 2
            },
            new ToDoTask
            {
                Title = "Test User Authentication",
                Description = "Perform tests on user login and authentication.",
                DueDate = DateTime.UtcNow.AddDays(-2),
                Priority = "High",
                IsCompleted = true,
                UserId = 3,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 2
            },
            new ToDoTask
            {
                Title = "Build User Profile Page",
                Description = "Create a user profile page for the app.",
                DueDate = DateTime.UtcNow.AddDays(9),
                Priority = "Low",
                IsCompleted = false,
                UserId = 4, 
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 3
            },
            new ToDoTask
            {
                Title = "Design Product Listing Page",
                Description = "Design the page for listing products.",
                DueDate = DateTime.UtcNow.AddDays(11),
                Priority = "Medium",
                IsCompleted = false,
                UserId = 4,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 3
            },
            new ToDoTask
            {
                Title = "Task4",
                Description = "Design the page for listing products.",
                DueDate = DateTime.UtcNow.AddDays(-4),
                Priority = "Medium",
                IsCompleted = false,
                UserId = 4,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 3
            },
            new ToDoTask
            {
                Title = "Design E-Commerce Checkout",
                Description = "Design the checkout page for the e-commerce app.",
                DueDate = DateTime.UtcNow.AddDays(12),
                Priority = "High",
                IsCompleted = false,
                UserId = 5,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 3
            },
            new ToDoTask
            {
                Title = "Task5",
                Description = "Design the page for listing products.",
                DueDate = DateTime.UtcNow.AddDays(-5),
                Priority = "Medium",
                IsCompleted = false,
                UserId = 5,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 3
            },
            new ToDoTask
            {
                Title = "Design Product Listing Page",
                Description = "Design the page for listing products.",
                DueDate = DateTime.UtcNow.AddDays(-3),
                Priority = "Medium",
                IsCompleted = false,
                UserId = 4,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                ProjectId = 3
            },
             new ToDoTask
             {
                 Title = "testTask",
                 Description = "Design the page for listing products.",
                 DueDate = DateTime.UtcNow.AddDays(-3),
                 Priority = "Medium",
                 IsCompleted = false,
                 UserId = 4,
                 CreatedOn = DateTime.UtcNow,
                 CreatedBy = "1",
                 ProjectId = 3
             }
        );
        context.SaveChanges();
    }

}
