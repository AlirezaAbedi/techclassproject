using Domain.Entity;
using Domain.Entity.Account;
using Domain.Entity.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Organization> Organizations { get; set; }
        DbSet<Domain.Entity.Account.Domain> Domains { get; set; }
        DbSet<UserFile> UserFiles { get; set; }

        Task<int> SaveChanges();
        
    }
}
