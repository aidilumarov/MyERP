using AutoMapper;
using MyERP.Application.Exceptions;
using MyERP.Application.Repositories;
using MyERP.Dtos;
using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyERP.Application.Services
{
    public class AbstractService<TDto, TEntity> 
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        protected AbstractService(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<List<TDto>> GetAsync()
        {
            var entities = await Repository.ListAsync();
            var entityDtos = new List<TDto>(entities.Count());
            Mapper.Map(entities, entityDtos);
            return entityDtos;
        }

        public async Task<TDto> GetAsync(Guid id)
        {
            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);
            }

            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto entityDto)
        {
            var entity = Mapper.Map<TDto, TEntity>(entityDto);
            await Repository.AddAsync(entity);
            entityDto.Id = entity.Id;
            return entityDto;
        }

        public virtual async Task<TDto> UpdateAsync(Guid id, TDto entityDto)
        {
            var entityToUpdate = await Repository.GetByIdAsync(id);

            if (entityToUpdate == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);
            }

            Mapper.Map(entityDto, entityToUpdate);
            await Repository.EditAsync(entityToUpdate);
            entityDto.Id = id;
            return entityDto;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entityToDelete = await Repository.GetByIdAsync(id);

            if (entityToDelete == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);
            }

            await Repository.DeleteAsync(entityToDelete);
            return true;
        }

        public virtual async Task<List<TDto>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var suitableItems = await Repository.ListAsync(predicate);
            var entityDtos = new List<TDto>(suitableItems.Count());
            Mapper.Map(suitableItems, entityDtos);

            return entityDtos;
        }

    }
}
