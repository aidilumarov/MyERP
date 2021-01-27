using AutoMapper;
using MyERP.Application.Repositories;
using MyERP.Dtos;
using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Application.Services
{
    public class OrderService : AbstractService<OrderDto, Order>
    {
        public OrderService(IRepository<Order> repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}
