using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase 
    {
        private IMediator _mediator;
        private IConfiguration _configuration;

        public BaseApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected IMediator Mediator => _mediator ??= HttpContext?.RequestServices?.GetService<IMediator>();

        


    }
}
