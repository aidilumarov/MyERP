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

        public DbSet<User> Users { get; set; }
    }
}
