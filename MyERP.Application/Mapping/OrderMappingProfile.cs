using AutoMapper;
using MyERP.Dtos;
using MyERP.Entities;

namespace MyERP.Application.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
