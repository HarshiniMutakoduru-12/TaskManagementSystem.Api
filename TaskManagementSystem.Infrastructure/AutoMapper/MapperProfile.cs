using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.DTOs.RequestDTO;
using TaskManagementSystem.Common.DTOs.ResponseDTO;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddUserResponseDto, User>()
                .ReverseMap();

            CreateMap<AddUserRequestDto, User>().ReverseMap();

            #region tasks

            CreateMap<AddTaskRequestDto, ToDoTask>()
                .ReverseMap();

            CreateMap<AddTaskResponseDto,ToDoTask>()
                .ReverseMap();

            CreateMap<TaskAssignedToUserResponseDto, ToDoTask>()
                .ReverseMap();

            //CreateMap<, UserCompletedTaskCountRespDto>()
            //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            //.ForMember(dest => dest.CompletedTaskCount, opt => opt.MapFrom(src => src.CompletedTaskCount));
            #endregion


            CreateMap<AddProjectRequestDto, Project>().ReverseMap();
            CreateMap<AddProjectResponseDto, Project>().ReverseMap();

        }
    }
}
