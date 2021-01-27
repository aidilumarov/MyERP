using System;
using Autofac;
using AutoMapper;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();
        }
    }
}
