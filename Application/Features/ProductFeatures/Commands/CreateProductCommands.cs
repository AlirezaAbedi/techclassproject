using Application.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand: IRequest<int>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateProductCommandHandler(IApplicationDbContext context)
            {
                _context= context;
            }
            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.ProductId = command.ProductId;
                product.ProductName = command.ProductName;
                product.SupplierId = command.SupplierId;
                product.CategoryId = command.CategoryId;

                _context.Products.Add(product);
                await _context.SaveChanges();
                return product.ProductId;
            }
        }

    }
}
