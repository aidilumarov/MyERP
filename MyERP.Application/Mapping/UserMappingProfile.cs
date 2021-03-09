using AutoMapper;
using MyERP.Dtos;
using MyERP.Entities;

namespace MyERP.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, User>()
                .ReverseMap();
        }
    }
}
