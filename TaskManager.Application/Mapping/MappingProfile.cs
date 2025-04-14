using AutoMapper;
using TaskManager.API.DTOs;
using TaskManager.API.DTOs.Task;
using TaskManager.API.DTOs.User;
using TaskManager.API.Models;
using Task = TaskManager.API.Models.Task;


namespace TaskManager.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserCreateDTO, User>();

            CreateMap<Task, TaskDTO>();
            CreateMap<TaskCreateDTO, Task>();

            CreateMap<Priority, PriorityDTO>();

        }
    }
}
