using AutoMapper;
using TaskManager.API.DTOs.User;
using TaskManager.API.Models;

namespace TaskManager.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserCreateDTO, User>();
        }
    }
}
