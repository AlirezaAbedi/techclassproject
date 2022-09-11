using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Model;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTestWebApi
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public async Task RegisterUser_CheckValidEmail()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            UserViewModel user = new UserViewModel
            {
                Email = "aabedi79.gmail.com",
                Lastname = "Abedi",
                Firstname = "Alireza",
                DomainTitle = "gmail.com"
            };

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/v1/User/RegisterUser", data);

            var result = JsonConvert.DeserializeObject<ServiceDto<int>>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(result?.Message, "Not Supported Email");

        }
    }
}