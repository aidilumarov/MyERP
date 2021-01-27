using Microsoft.EntityFrameworkCore;
using MyERP.Application.Repositories;
using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Infrastructure.EFCore
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasData(
                new Order { Id = Guid.NewGuid(), Date = DateTime.Today, Price = 500 },
                new Order { Id = Guid.NewGuid(), Date = DateTime.Today, Price = 500 },
                new Order { Id = Guid.NewGuid(), Date = DateTime.Today, Price = 500}
            );
        }
    }
}
