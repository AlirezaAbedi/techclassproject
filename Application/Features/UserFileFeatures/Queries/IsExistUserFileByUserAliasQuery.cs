using Application.Interfaces;
using Domain.Entity;
using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFileFeatures.Queries
{
    public class IsExistUserFileByUserAliasQuery: IRequest<ServiceDto<bool>>
    {
        public string Aliasname { get; set; }
        public int UserId { get; set; }
        public class IsExistUserFileByUserAliasQueryHandler : IRequestHandler<IsExistUserFileByUserAliasQuery, ServiceDto<bool>>
        {
            private readonly IApplicationDbContext _context;
            public IsExistUserFileByUserAliasQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ServiceDto<bool>> Handle(IsExistUserFileByUserAliasQuery query, CancellationToken cancellationToken)
            {
                var data = _context.UserFiles.
                    Where(x => x.Aliasname == query.Aliasname
                                && x.AuthUserId == query.UserId).FirstOrDefault();

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
