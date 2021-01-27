using Autofac;
using AutoMapper;
using MyERP.Web.StartupConfig;

namespace MyERP.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => 
            {
                return AutomapperConfig.RegisterAutomapper();
            }).As<IMapper>().SingleInstance();
        }
    }
}
