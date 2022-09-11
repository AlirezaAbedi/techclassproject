using Application.Interfaces;
using Domain.Entity.Account;
using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands
{
    public class CreateFileUserCommands : IRequest<ServiceDto<int>>
    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public class CreateUserCommandsHandler : IRequestHandler<CreateFileUserCommands, ServiceDto<int>>

        {
            private readonly IApplicationDbContext _context;
            public CreateUserCommandsHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            //async Task<ServiceDto<CreatedModel>> IRequestHandler<PayRequestCommand, ServiceDto<CreatedModel>>.Handle(PayRequestCommand request, CancellationToken cancellationToken)
            public async Task<ServiceDto<int>> Handle(CreateFileUserCommands request, CancellationToken cancellationToken)
            {
                var user = new User();
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.Email = request.Email;
                user.dateCreated = DateTime.Now;
                user.dateApproved = DateTime.Now;
                user.datelastModified = DateTime.Now;
                user.authOrganizationsId = 1;
                user.authDomainId = 1;
                user.authRolesId = 1;
                

                _context.Users.Add(user);
                await _context.SaveChanges();

                return new ServiceDto<int>
                {
                    Data = user.Id,
                    Status = 1,
                    Message = "User is Created"
                };


            }
        }
    }
}
