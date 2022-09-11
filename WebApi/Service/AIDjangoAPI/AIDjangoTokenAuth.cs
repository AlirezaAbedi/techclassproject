using Model;
using Model.User;
using Model.UserFile;
using RestSharp;
using System.Text.Json;
using WebApi.Service.AIDjangoAPI;

namespace WebApi.Service.TokenAuth
{
    public class AIDjangoTokenAuth
    {
        public ServiceDto<AIAPIToken> GetTokenAuthByUserPass(AIAPIToken cred, IConfiguration configuration)
        {
            RestClient client = AIDjangoAPIBase.GetClientConnection(configuration, "/todos/login/");

            RestRequest request = new RestRequest();
            request.Method = Method.Post;
            request.AddJsonBody(new { username = cred.Username, password = cred.Password });
            RestResponse response = client.Execute(request);

            return new ServiceDto<AIAPIToken>()
            {
                Data = JsonSerializer.Deserialize<AIAPIToken>(response.Content),
                Status = 1
            };
        }
    }
}
