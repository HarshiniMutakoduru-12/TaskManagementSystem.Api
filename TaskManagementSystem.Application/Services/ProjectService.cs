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
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repos.IRepository;
using TaskManagementSystem.Data.Repos.Repository;

namespace TaskManagementSystem.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository,IMapper mapper) {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        // Add Project
        public async Task<string> AddProjectAsync(AddProjectRequestDto project)
        {
            var projects = _mapper.Map<Project>(project);
            projects.CreatedOn = DateTime.UtcNow;
            projects.CreatedBy = "1";
            await _projectRepository.AddAsync(projects);
            return ApiErrorCodeMessages.ProjectAddedSuccessfully;
        }
        public async Task<List<AddProjectResponseDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAll().ToListAsync();
            return _mapper.Map<List<AddProjectResponseDto>>(projects);
        }
    }
}
