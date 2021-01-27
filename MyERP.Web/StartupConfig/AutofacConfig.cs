using System;
using Autofac;
using Autofac.Core.Registration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyERP.Application;
using MyERP.Infrastructure;

namespace MyERP.Web.StartupConfig
{
    public static class AutofacConfig
    {
        public static ContainerBuilder RegisterRequiredModules(this ContainerBuilder builder)
        {
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<WebModule>();

            return builder;
        }
    }
}
