using Application.Interfaces;
using Domain.Entity.Account;
using Domain.Entity.File;
using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFileFeatures.Commands
{
    public class CreateUserFileCommands : IRequest<ServiceDto<int>>
    {

        public string FileName { get; set; }
        public string AliasName { get; set; }
        public string ObjectId { get; set; }

        public class CreateUserFileCommandsHandler : IRequestHandler<CreateUserFileCommands, ServiceDto<int>>

        {
            private readonly IApplicationDbContext _context;
            public CreateUserFileCommandsHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ServiceDto<int>> Handle(CreateUserFileCommands request, CancellationToken cancellationToken)
            {
                var userFile = new UserFile();
                userFile.Filename = request.FileName;
                userFile.Aliasname = request.AliasName;
                userFile.MongoDbObjectId = request.ObjectId;
                
                

                _context.UserFiles.Add(userFile);
                await _context.SaveChanges();

                return new ServiceDto<int>
                {
                    Data = userFile.Id,
                    Status = 1,
                    Message = "User File is Created"
                };


            }

        }
    }
}
