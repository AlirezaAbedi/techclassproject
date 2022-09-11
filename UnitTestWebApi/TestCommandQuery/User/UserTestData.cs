using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Account;

namespace UnitTestWebApi.User
{
    public static class UserTestData
    {
        public static IQueryable<Organization> Organizations
        {
            get
            {
                return new List<Organization>
                {
                    new Organization { Id = 1, OrgName = "gmail.com"},
                    new Organization { Id = 2, OrgName = "neda.com" }
                }
                .AsQueryable();
            }
        }

        public static IQueryable<Domain.Entity.Account.User> Users
        {
            get
            {
                return new List<Domain.Entity.Account.User>
                {
                    new Domain.Entity.Account.User{Id = 1, authDomainId=1, Email="aabedi79@gmail.com"},
                }.AsQueryable();
            }
        }

        public static IQueryable<Domain.Entity.Account.Domain> Domains
        {
            get
            {
                return new List<Domain.Entity.Account.Domain>
                {
                    new Domain.Entity.Account.Domain{Id = 1, DomainTitle="google" },
                }.AsQueryable();
            }
        }
    }
}
