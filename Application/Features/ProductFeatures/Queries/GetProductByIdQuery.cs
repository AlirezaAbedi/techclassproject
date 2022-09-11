//using Application.Interfaces;
//using Domain.Entity;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Features.ProductFeatures.Queries
//{
//    public class IsExistUserFileByUserAliasQuery: IRequest<Product>
//    {
//        public int ProductId { get; set; }
//        public class GetProductByIdQueryHandler : IRequestHandler<IsExistUserFileByUserAliasQuery, Product>
//        {
//            private readonly IApplicationDbContext _context;
//            public GetProductByIdQueryHandler(IApplicationDbContext context)
//            {
//                _context = context;
//            }
//            public async Task<Product> Handle(IsExistUserFileByUserAliasQuery query, CancellationToken cancellationToken)
//            {
//                var product = _context.Products.Where(a => a.ProductId == query.ProductId).FirstOrDefault();
//                if (product == null) return null;
//                return product;
//            }
//        }
//    }
//}
