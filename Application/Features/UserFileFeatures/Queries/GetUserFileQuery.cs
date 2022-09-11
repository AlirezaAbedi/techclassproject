using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Filter;
using Model.UserFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.UserFile;

namespace Application.Features.UserFileFeatures.Queries
{
    public class GetUserFileQuery: IRequest<ServiceDto<PagedResultDto<UserFileViewResponseModel>>>
    {
        public PaginationFilter paginationFilter { get; set; }
        public class GetUserFileQueryHandler : IRequestHandler<GetUserFileQuery, ServiceDto<PagedResultDto<UserFileViewResponseModel>>>
        {
            private readonly IApplicationDbContext _context;
            public GetUserFileQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ServiceDto<PagedResultDto<UserFileViewResponseModel>>> Handle(GetUserFileQuery query, CancellationToken cancellationToken)
            {
                var data = _context.UserFiles
                        .Skip((query.paginationFilter.PageNumber - 1) * query.paginationFilter.PageSize)
                        .Take(query.paginationFilter.PageSize);

                var result = new List<UserFileViewResponseModel>();
                foreach (var item in data)
                {
                    result.Add(item.Adapt<UserFileViewResponseModel>());
                }


                return new ServiceDto<PagedResultDto<UserFileViewResponseModel>>()
                {
                    Data = new PagedResultDto<UserFileViewResponseModel>()
                    {
                        Items = result,
                        TotalCount = data.Count(),
                        PageNumber = query.paginationFilter.PageNumber,
                    }
                    
                };
            }
        }

    }
}
