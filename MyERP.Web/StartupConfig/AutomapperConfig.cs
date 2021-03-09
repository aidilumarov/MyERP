using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyERP.Application.Mapping;

namespace MyERP.Web.StartupConfig
{
    public static class AutomapperConfig
    {
        public static IMapper RegisterAutomapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<UserMappingProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
