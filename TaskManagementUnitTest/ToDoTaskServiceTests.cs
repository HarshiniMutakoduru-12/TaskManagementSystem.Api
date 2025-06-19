using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Common.Constants;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.Exceptions;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repos.IRepository;
using Xunit.Sdk;

namespace TaskManagementUnitTest
{
    public class ToDoTaskServiceTests
    {
        private readonly Mock<IToDoTaskRepository> _mockToDoTaskRepo;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IProjectRepository> _mockProjectRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ToDoTaskService _service;

        public ToDoTaskServiceTests()
        {
            _mockToDoTaskRepo = new Mock<IToDoTaskRepository>();
            _mockUserRepo = new Mock<IUserRepository>();
            _mockProjectRepo = new Mock<IProjectRepository>();
            _mockMapper = new Mock<IMapper>();

            _service = new ToDoTaskService(
                _mockToDoTaskRepo.Object,
                _mockMapper.Object,
                _mockUserRepo.Object,
                _mockProjectRepo.Object
            );
        }

        [Fact]
        public async Task AddTaskAsync_ShouldAddTaskSuccessfully_WhenValidRequest()
        {
            // Arrange
            var addTaskRequest = new AddTaskRequestDto
            {
                UserId = 1,
                Title = "Test Task"
            };

            var user = new User { UserId = 1 };
            var task = new ToDoTask { TaskId = 1, UserId = 1 };

            _mockUserRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _mockToDoTaskRepo.Setup(r => r.AddAsync(It.IsAny<ToDoTask>()))
                  .ReturnsAsync(task);
            _mockMapper.Setup(m => m.Map<ToDoTask>(addTaskRequest)).Returns(task);

            // Act
            var result = await _service.AddTaskAsync(addTaskRequest);

            // Assert
            result.Should().Be(ApiErrorCodeMessages.TaskAddedSuccessfully);
            _mockToDoTaskRepo.Verify(r => r.AddAsync(It.IsAny<ToDoTask>()), Times.Once);
        }

        [Fact]
        public async Task AddTaskAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var addTaskRequest = new AddTaskRequestDto
            {
                UserId = 999, // Invalid user
                Title = "Test Task"
            };

            _mockUserRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(() => _service.AddTaskAsync(addTaskRequest));
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldUpdateTaskSuccessfully_WhenValidRequest()
        {
            // Arrange
            var updateTaskRequest = new UpdateTaskRequestDto
            {
                Title = "Updated Task"
            };

            var task = new ToDoTask
            {
                TaskId = 1,
                Title = "Old Task",
                UserId = 1
            };

            _mockToDoTaskRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(task);
            _mockToDoTaskRepo.Setup(r => r.UpdateAsync(It.IsAny<ToDoTask>())).ReturnsAsync(task);

            // Act
            var result = await _service.UpdateTaskAsync(1, updateTaskRequest);

            // Assert
            result.Should().Be(ApiErrorCodeMessages.TaskUpdatedSuccessfully);
            task.Title.Should().Be("Updated Task");
            _mockToDoTaskRepo.Verify(r => r.UpdateAsync(It.IsAny<ToDoTask>()), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldThrowException_WhenTaskNotFound()
        {
            // Arrange
            var updateTaskRequest = new UpdateTaskRequestDto
            {
                Title = "Updated Task"
            };

            _mockToDoTaskRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((ToDoTask)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(() => _service.UpdateTaskAsync(999, updateTaskRequest));
        }
    }
}
