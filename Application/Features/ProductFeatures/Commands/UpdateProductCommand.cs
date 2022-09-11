using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand: IRequest<int>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {

            private readonly IApplicationDbContext _context;
            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.ProductId == command.ProductId).FirstOrDefault();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.ProductName = command.ProductName;
                    product.SupplierId = command.SupplierId;
                    product.CategoryId = command.CategoryId;
                    
                    await _context.SaveChanges();
                    return product.ProductId;
                }
            }
        }
    }
}
