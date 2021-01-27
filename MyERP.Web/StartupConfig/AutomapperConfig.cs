using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyERP.Application.Mapping;

namespace MyERP.Web.StartupConfig
{
    public static class AutomapperConfig
    {
        public static IServiceCollection AddAutoMapperWithProfiles(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<OrderMappingProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return services.AddSingleton(mapper);
        }
    }
}
