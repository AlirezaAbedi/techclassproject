using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;

namespace WebApi.Service
{
    public class GoogleOAuthService
    {
        [GoogleScopedAuthorize(PeopleServiceService.ScopeConstants.UserinfoEmail)]
        public async Task<string> GetGoogleOAuthUserProfile(IGoogleAuthProvider auth)
        {
            var cred = await auth.GetCredentialAsync();
            var service = new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred
            });


            var request = service.People.Get("people/me");
            request.PersonFields = "names";
            var person = await request.ExecuteAsync();

            return null;
        }
    }
}
