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

        public async Task<TDto> GetAsync(int id)
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
            await Repository.AddAsync(entity, true);
            return entityDto;
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TDto> entityDtos)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(entityDtos);
            await Repository.AddAsync(entities, true);
        }

        public virtual async Task<TDto> UpdateAsync(int id, TDto entityDto)
        {
            var entityToUpdate = await Repository.GetByIdAsync(id);

            if (entityToUpdate == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);
            }

            Mapper.Map(entityDto, entityToUpdate);
            await Repository.EditAsync(entityToUpdate);
            return Mapper.Map(entityToUpdate, entityDto);
        }

        public virtual async Task<bool> DeleteAsync(int id)
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
