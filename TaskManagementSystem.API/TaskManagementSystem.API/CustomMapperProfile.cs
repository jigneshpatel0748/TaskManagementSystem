using AutoMapper;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Dto.Role;
using TaskManagementSystem.Dto.User;
using TaskManagementSystem.Dto.Project;

namespace TaskManagementSystem.API
{
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<LoginResponseDto, Users>().ReverseMap();
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<UserRequestDto, Users>().ReverseMap();
            CreateMap<RoleDto, Roles>().ReverseMap();
            CreateMap<RoleRequestDto, Roles>().ReverseMap();
            CreateMap<ProjectDto, Projects>().ReverseMap();
            CreateMap<ProjectRequestDto, Projects>().ReverseMap();
            CreateMap<TaskDto, Tasks>().ReverseMap();
            CreateMap<TaskRequestDto, Tasks>().ReverseMap();
            CreateMap<TaskNoteDto, TaskNotes>().ReverseMap();
            CreateMap<TaskNoteRequestDto, TaskNotes>().ReverseMap();
        }
    }
}
