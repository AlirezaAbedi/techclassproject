using Domain.Entity.Account;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi.Controllers.v1
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class GoogleController : ControllerBase
    {
        //[HttpGet]
        //public  string GetData()
        //{
        //    return "Sample Data"; 
        //}

        [HttpGet]
        [GoogleScopedAuthorize(PeopleServiceService.ScopeConstants.UserinfoEmail)]
        public async Task<ServiceDto<GoogleUser>> GoogleUserProfile([FromServices] IGoogleAuthProvider auth)
        {
            var cred = await auth.GetCredentialAsync();
            var service = new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred
            });


            var request = service.People.Get("people/me");
            request.PersonFields = "emailAddresses";
            var person = await request.ExecuteAsync();

            return new ServiceDto<GoogleUser>
            {
                Data = new GoogleUser
                {
                    Email = person.EmailAddresses.FirstOrDefault().Value
                },
                Status = 1
            };

        }
    }
}
