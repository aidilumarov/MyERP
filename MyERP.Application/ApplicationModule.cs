using System;
using Autofac;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using MyERP.Application.Services;
using MyERP.Dtos;

namespace MyERP.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderService>().InstancePerLifetimeScope();
            builder.RegisterType<ExcelReportWriter>().As<IReportWriter<OrderDto>>().SingleInstance();
        }
    }
}
