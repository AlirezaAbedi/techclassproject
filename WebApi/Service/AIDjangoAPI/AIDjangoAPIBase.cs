using RestSharp;

namespace WebApi.Service.AIDjangoAPI
{
    public static class AIDjangoAPIBase
    {

        public static RestClient GetClientConnection(IConfiguration configuration,string relatedPath)
        {
            var options = new RestClientOptions(
                configuration.GetSection("DjangoAPI").GetSection("IPAddress").Value + relatedPath)
            {
                ThrowOnAnyError = true,
                MaxTimeout = int.Parse(
                    configuration.GetSection("DjangoAPI").GetSection("Timeout").Value),
            };
            return new RestClient(options);
        }

        public static RestRequest PoulatedRequest(RestRequest request, MemoryStream memoryStream, IFormFile file)
        {
            request.Method = Method.Post;
            request.AddBody(new { myFile = memoryStream.ToArray() });
            request.AlwaysMultipartFormData = true;
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("file", memoryStream.ToArray(), "multipart/form-data");
            request.AddParameter("multipart/form-data", file.FileName, ParameterType.RequestBody);

            return request;
        }
    }
}
