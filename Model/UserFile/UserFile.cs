using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserFile
{
    public class UserFileViewRequestModel
    {
        public string Email { get; set; }
        public string? Aliasname { get; set; }
        public IFormFile? FileObject { get; set; }
        public string? MongoDbObjectId { get; set; }
    }

    public class UserFileViewResponseModel
    {
        public string? Filename { get; set; }
        public string? FileExtension { get; set; }
        public string? FileStatus { get; set; }
        public string? FileType { get; set; }

        public string? MongoDbObjectId { get; set; }

        public DateTime? UploadDate { get; set; }

        public DateTime? ProcessDate { get; set; }

        public string? Aliasname { get; set; }

        public int? AuthUserId { get; set; }
    }

    public class UserFileInput
    {
        public string email { get; set; }
        public string? aliasName { get; set; }
    }

    
}
