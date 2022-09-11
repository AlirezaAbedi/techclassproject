using Application.Interfaces;
using Domain.Entity;
using Domain.Entity.Account;
using Domain.Entity.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Domain.Entity.Account.Domain> Domains { get; set; }

        public DbSet<UserFile> UserFiles { get; set; }


        public DbSet<Organization> Organizations { get; set; }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("auth_users");
            modelBuilder.Entity<UserFile>().ToTable("user_files");
            modelBuilder.Entity<Organization>().ToTable("auth_organizations");
            modelBuilder.Entity<Domain.Entity.Account.Domain>().ToTable("auth_domain");
        }

    }
}
