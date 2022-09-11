using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.UserFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    public class FindUserByEmailQueries : IRequest<ServiceDto<PagedResultDto<UserViewModel>>>
    {
        public string Email { get; set; }
        public class FindUserByEmailQueriesHandler : IRequestHandler<FindUserByEmailQueries, ServiceDto<PagedResultDto<UserViewModel>>>
        {
            private readonly IApplicationDbContext _context;
            public FindUserByEmailQueriesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ServiceDto<PagedResultDto<UserViewModel>>> Handle(FindUserByEmailQueries request, CancellationToken cancellationToken)
            {
                var data = (from u in _context.Users
                              join d in _context.Domains
                              on u.authDomainId equals d.Id
                              where u.Email == request.Email
                              select new
                              {
                                  DomainId = d.Id,
                                  DomainTitle = d.DomainTitle,
                                  Email = u.Email,
                                  isApproved = u.isApproved,
                                  UserId = u.Id
                              });


                var result = new List<UserViewModel>();

                foreach (var item in data)
                {
                    result.Add(item.Adapt<UserViewModel>());
                }

                return new ServiceDto<PagedResultDto<UserViewModel>>()
                {
                    Data = new PagedResultDto<UserViewModel>()
                    {
                        Items = result,
                        TotalCount = data.Count()
                    }
                };


            }
        }
    }
}
