using AutoMapper;
using MyERP.Application.Repositories;
using MyERP.Dtos;
using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Application.Services
{
    public class UserService : AbstractService<UserDto, User>
    {
        public UserService(IRepository<User> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public virtual async Task CreateOrUpdate(IEnumerable<UserDto> userDtos)
        {
            var entities = Mapper.Map<IEnumerable<User>>(userDtos);
            var entitiesToBeCreated = new List<User>();

            foreach (var entity in entities)
            {
                var existingEntity = await Repository.GetByIdAsync(entity.UserId);
                
                if (existingEntity == null)
                {
                    entitiesToBeCreated.Add(entity);
                }

                else
                {
                    existingEntity.DateRegistration = entity.DateRegistration;
                    existingEntity.DateLastActivity = entity.DateLastActivity;
                    await Repository.EditAsync(existingEntity);
                }
            }

            await Repository.AddAsync(entitiesToBeCreated, true);
        }

        public virtual async Task<int> CalculateRollingRetention(int days)
        {
            // (Users that returned in X days or later) 
            // divided by Users that installed the app X days before or earlier * 100%
            var users = await Repository.ListAsync();

            var usersReturned = users
                .Where(user => (user.DateLastActivity - user.DateRegistration).TotalDays >= days).ToList();

            var usersInstalled = users
                .Where(user => (DateTime.Now - user.DateRegistration).TotalDays >= days).ToList();

            var rollingRetention = usersReturned.Count() / usersInstalled.Count();

            return rollingRetention;
        }

        public virtual async Task<Dictionary<int, int>> GetUserLifeTimeDistribution()
        {
            var users = await Repository.ListAsync();
            var distribution = new Dictionary<int, int>();

            foreach (var user in users)
            {
                var daysFromRegistrationToLastActivity = (int)(user.DateLastActivity - user.DateRegistration).TotalDays;

                if (distribution.ContainsKey(daysFromRegistrationToLastActivity))
                {
                    distribution[daysFromRegistrationToLastActivity] += 1;
                }

                else
                {
                    distribution.Add(daysFromRegistrationToLastActivity, 1);
                }
            }

            return distribution;
        }
     }
}
