using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MyERP.Web.StartupConfig
{
    public static class AutofacConfig
    {
        public static IServiceProvider GetAutofacServiceProviderWithRegisteredModules(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);
            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
