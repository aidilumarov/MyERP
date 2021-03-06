using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MyERP.Application.Repositories;
using MyERP.Infrastructure.EFCore;
using MyERP.Web.StartupConfig;
using System;
using Autofac;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace MyERP.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var connectionString = Configuration["ConnectionStrings:LocalDbMSSQLExpress"];
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MyERP.Infrastructure")));

            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../frontend/build";
            });
            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModules();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../frontend";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
            
        }
    }
}
