using Application.Features.UserFeatures.Queries;
using Application.Interfaces;
using Domain.Entity.Account;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestWebApi.Base;
using static Application.Features.UserFeatures.Queries.AcceptedDomainQueries;
using static Application.Features.UserFeatures.Queries.FindUserByEmailQueries;

namespace UnitTestWebApi.User
{
    public class UserHandlerTests
    {
        [Theory]
        [InlineData("gmail.com_")]
        public async void GetAcceptedDomainByEmailExtension(string emailExtension)
        {
            var DbContext = new Mock<IApplicationDbContext>();

            DbContext.SetupGet(x => x.Organizations).Returns
                (TestFunctions.GetDbSet<Organization>(UserTestData.Organizations).Object);

            var UserHandler = new AcceptedDoaminQueriesHandler(DbContext.Object);
            AcceptedDomainQueries request = new AcceptedDomainQueries
            {
                EmailExtension = emailExtension
            };

            var Result = await UserHandler.Handle(request, new CancellationToken());
            Assert.True(Result.Data);
        }

        [Theory]
        [InlineData("aabedi79@gmail.com")]
        public async void FindUserByEmail(string email)
        {
            var DbContext = new Mock<IApplicationDbContext>();
            var UserHandler = new FindUserByEmailQueriesHandler(DbContext.Object);

            DbContext.SetupGet(x => x.Users).Returns
                (TestFunctions.GetDbSet<Domain.Entity.Account.User>(UserTestData.Users).Object);

            DbContext.SetupGet(x => x.Domains).Returns
                (TestFunctions.GetDbSet<Domain.Entity.Account.Domain>(UserTestData.Domains).Object);


            FindUserByEmailQueries request = new FindUserByEmailQueries
            {
                Email = email
            };

            var Result = await UserHandler.Handle(request, new CancellationToken());
            Assert.NotNull(Result.Data.Items);

        }
    }
}
