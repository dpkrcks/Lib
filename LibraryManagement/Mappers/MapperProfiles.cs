using AutoMapper;
using LibraryManagement.DTOs.RequestDTOs;
using LibraryManagement.DTOs.ResponseDTOs;
using LibraryManagement.Models;

namespace LibraryManagement.Mappers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User , UserResponseDto>().ReverseMap();
            CreateMap<User,UserLoginResponseDto>().ReverseMap();
        
        }
    }
}
