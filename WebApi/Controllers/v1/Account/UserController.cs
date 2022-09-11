using Application.Features.UserFeatures.Commands;
using Application.Features.UserFeatures.Queries;
using CommonLibrary.String;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.PeopleService.v1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Security.AccessControl;

namespace WebApi.Controllers.v1.Account
{
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private IConfiguration _configuration;
        //private IMediator _mediator;
        public UserController(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceDto<int>>> GetValues()
        {
            return Ok(new ServiceDto<int>
            {
                Status = 0,
                Message = "Not Supported Email"
            });
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto<int>>> RegisterUser(UserViewModel user)
        {
            WorkingWithEmailContext workingWithEmailContext = new WorkingWithEmailContext();
            ServiceDto<string> email = workingWithEmailContext.ExtractDomainFromEmail(user.Email);

            if (email.Status == 0)
            {
                return Ok(new ServiceDto<int>
                {
                    Status = 0,
                    Message = "Not Supported Email"
                });
            }


            Task<ServiceDto<bool>> acceptedDomain = Mediator.Send(new AcceptedDomainQueries
            {
                EmailExtension = email.Data
            });

            if (acceptedDomain.Result.Data)
            {

                Task<ServiceDto<PagedResultDto<UserViewModel>>> isExistUser = 
                    Mediator.Send(new FindUserByEmailQueries
                {
                    Email = user.Email
                });

                if (isExistUser.Result.Data.TotalCount == 0)
                {
                    return await Mediator.Send(new CreateFileUserCommands
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Email = user.Email
                    });
                }
                else
                {
                    return new ServiceDto<int>
                    {
                        Status = 0,
                        Errors = new List<string>()
                    {
                        "Duplicated Users"
                    }
                    };
                }

            }
            else
            {
                return new ServiceDto<int>
                {
                    Status = 0,
                    Errors = new List<string>()
                    {
                        "Not Supported Domain"
                    }
                };
            }
        }

        [HttpPost]
        [GoogleScopedAuthorize(PeopleServiceService.ScopeConstants.UserinfoEmail)]
        public async Task<ServiceDto<PagedResultDto<UserViewModel>>> LoginUser(UserViewModel user,
            [FromServices] IGoogleAuthProvider auth)
        {
            
            
            Task<ServiceDto<PagedResultDto<UserViewModel>>> loginUser =
                    Mediator.Send(new FindUserByEmailQueries
                    {
                        Email = user.Email
                    });

            if (loginUser.Result.Data.TotalCount == 0)
            {
                return new ServiceDto<PagedResultDto<UserViewModel>>
                {
                    Data = loginUser.Result.Data,
                    Status = 0,
                    Message = "Not Existed User"
                };
            }

            if (loginUser.Result.Data.Items.FirstOrDefault().IsApproved ==0)
            {
                return new ServiceDto<PagedResultDto<UserViewModel>>
                {
                    Status = 0,
                    Message = "Not Approved User"
                };
            }

            return new ServiceDto<PagedResultDto<UserViewModel>>
            {
                Data = loginUser.Result.Data,
                Status = 1,
            };
        }
    }
}
