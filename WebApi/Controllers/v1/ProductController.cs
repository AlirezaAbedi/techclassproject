using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {

        public ProductController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSamples()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        //[HttpPost]
        //[GoogleScopedAuthorize(PeopleServiceService.ScopeConstants.UserinfoProfile)]
        //public async Task<IActionResult> UserProfile([FromServices] IGoogleAuthProvider auth)
        //{
        //    var cred = auth.GetCredentialAsync();
        //    var service = new PeopleServiceService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = (Google.Apis.Http.IConfigurableHttpClientInitializer)cred
        //    });

        //    var request = service.People.Get(resourceName: "people/me");
        //    request.PersonFields = "names";
        //    var person = await request.ExecuteAsync();

        //    return Ok(await Mediator.Send(request));
        //}

        [HttpGet]
        [GoogleScopedAuthorize(PeopleServiceService.ScopeConstants.UserinfoEmail)]
        public async Task<object> UserProfile([FromServices] IGoogleAuthProvider auth)
        {
            var cred = await auth.GetCredentialAsync();
            var service = new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred
            });


            var request = service.People.Get("people/me");
            request.PersonFields = "emailAddresses";
            var person = await request.ExecuteAsync();

            return null;
        }



        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            return null;//Ok(await Mediator.Send(new IsExistUserFileByUserAliasQuery { ProductId = productId }));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { ProductId = productId }));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int productId, UpdateProductCommand command)
        {
            if (productId != command.ProductId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

    }
}
