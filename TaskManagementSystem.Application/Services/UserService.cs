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

namespace TaskManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;   
        }
       

        // Create User
        public async Task<string> AddUserAsync(AddUserRequestDto user)
        {            
            var users = _mapper.Map<User>(user);
            users.CreatedOn = DateTime.UtcNow;
            users.CreatedBy = "1"; 
            await _userRepository.AddAsync(users);
            return ApiErrorCodeMessages.UserAddedSuccessfully;
        }

        // Get All Users
        public async Task<List<AddUserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAll().Where(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<AddUserResponseDto>>(users);
        }

    }
}
