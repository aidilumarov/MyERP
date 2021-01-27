using MyERP.Dtos;
using MyERP.Entities;
using System;

namespace MyERP.Application.Services
{
    public class AbstractService<TDto, TEntity> 
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
    }
}
