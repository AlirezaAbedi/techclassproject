using Model;
using Model.UserFile;
using RestSharp;
using System.Text.Json;

namespace WebApi.Service.AIDjangoAPI
{
    public class AIDjangoAPIUpload
    {
        public ServiceDto<UserFileViewResponseModel> UploadUserFiletoAIService(IFormFile file, IConfiguration configuration)
        {
            RestClient client = AIDjangoAPIBase.GetClientConnection(configuration, "upload/");

            var memoryStream = new MemoryStream();
            file.CopyToAsync(memoryStream);
            RestRequest request = AIDjangoAPIBase.PoulatedRequest(new RestRequest(), memoryStream, file);
            RestResponse response = client.Execute(request);

            return new ServiceDto<UserFileViewResponseModel>()
            {
                Data = JsonSerializer.Deserialize<UserFileViewResponseModel>(response.Content),
                Status = 1
            };

        }
    }
}
