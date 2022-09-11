using Application.Interfaces;
using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    public class AcceptedDomainQueries: IRequest<ServiceDto<bool>>
    {
        public string EmailExtension { get; set; }
        public class AcceptedDoaminQueriesHandler : IRequestHandler<AcceptedDomainQueries, ServiceDto<bool>>
        {
            private readonly IApplicationDbContext _context;
            public AcceptedDoaminQueriesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ServiceDto<bool>> Handle(AcceptedDomainQueries request, 
                CancellationToken cancellationToken)
            {
                var data = _context.Organizations
                    .Where(x => x.OrgName.Equals(request.EmailExtension)).FirstOrDefault();

                
                if (data != null)
                {
                    return new ServiceDto<bool>
                    {
                        Data = true
                    };
                }
                else
                {
                    return new ServiceDto<bool>
                    {
                        Data = false
                    };
                }
            }
        }
    }
}
