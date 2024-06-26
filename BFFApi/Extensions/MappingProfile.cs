namespace BFFApi;
using Entities;
using Shared.DTOs.UserDTOs;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
    }
}