using Application.Features.UserFeatures.Queries;
using Application.Features.UserFileFeatures.Commands;
using Application.Features.UserFileFeatures.Queries;
using CommonLibrary.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Filter;
using Model.User;
using Model.UserFile;
using RestSharp;
using WebApi.Service.AIDjangoAPI;
using WebApi.Service.TokenAuth;

namespace WebApi.Controllers.v1.FileProcess
{
    [ApiVersion("1.0")]
    public class UserFileController : BaseApiController
    {
        private IConfiguration _configuration;

        public UserFileController(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }



        [HttpPost]
        public async Task<bool> GetUserFiles()
        {
            Task<ServiceDto<PagedResultDto<UserFileViewResponseModel>>> userFiles =
                    Mediator.Send(new GetUserFileQuery
                    {
                        paginationFilter = new PaginationFilter
                        {
                            PageNumber = 1,
                            PageSize = 2,
                        }
                    });

            return false;
        }

        [HttpPost]
        public async Task<ServiceDto<int>> ProcessUserFiles(
            [FromQuery] UserFileInput userFile,
            [FromForm] IFormFile file)
        {
            // First Level File Validation

            if (file.Length /1024 > 1024)
            {
                return new ServiceDto<int>
                {
                    Status = 0,
                    Message = "Higher Than Max Size"
                };
            }

            if (file.ContentType == "application/csv" &&
                file.FileName.Split('.')[1] != "csv"
                )
            {
                CsvValidation cSVValidation = new CsvValidation();
                if (!cSVValidation.CheckFileisValidatedCSV(file).Data)
                {
                    return new ServiceDto<int>
                    {
                        Status = 0,
                        Message = cSVValidation.CheckFileisValidatedCSV(file).Message
                    };
                }
            }
            else if (file.ContentType == 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                && file.FileName.Split('.')[1] != "xlsx")
            {
                ExcelValidation excelValidation = new ExcelValidation();
                if (!excelValidation.CheckFileisValidatedExcel(file).Data)
                {
                    return new ServiceDto<int>
                    {
                        Status = 0,
                        Message = excelValidation.CheckFileisValidatedExcel(file).Message
                    };
                }
            }
            else
            {
                return new ServiceDto<int>
                {
                    Status = 0,
                    Message = "Not Supported File"
                };
            }



            // Is Existed by Alias Name & Email
            Task<ServiceDto<PagedResultDto<UserViewModel>>> userByEmail =
                    Mediator.Send(new FindUserByEmailQueries
                    {
                        Email = userFile.email
                    });

            Task<ServiceDto<bool>> isExistFileByAliasUserId = Mediator.Send(new IsExistUserFileByUserAliasQuery
            {
                Aliasname = userFile.aliasName,
                UserId = userByEmail.Result.Data.Items.FirstOrDefault().UserId
            });

            if (isExistFileByAliasUserId.Result.Data)
            {
                return new ServiceDto<int>()
                {
                    Status = 0,
                    Message = "Existed File with same Alias Name & Username"
                };
            }


            //AIDjangoAPI djangoAPI = new AIDjangoAPI();
            //ServiceDto<UserFileViewModel> uploadDjangoAPI = djangoAPI.UploadUserFiletoAIService(file);


            // Call Django API Auth Token
            AIDjangoTokenAuth aIDjangoTokenAuth = new AIDjangoTokenAuth();
            ServiceDto<AIAPIToken> apiToken = aIDjangoTokenAuth.GetTokenAuthByUserPass(new Model.User.AIAPIToken
            {
                Username = "admin",
                Password = "admin"
            }, _configuration);


            return  Mediator.Send(new CreateUserFileCommands
            {
                AliasName = userFile.aliasName,
                FileName = file.FileName,
                ObjectId = ""//uploadDjangoAPI.Data.objectId
            }).Result;

        }

    }
}
