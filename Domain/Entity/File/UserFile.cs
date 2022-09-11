using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.File
{
    public class UserFile: BaseEntity
    {
        [Column("file_name", TypeName = "string")]
        public string? Filename { get; set; }

        [Column("file_extension", TypeName = "string")]
        public string? FileExtension { get; set; }

        [Column("file_status", TypeName = "string")]
        public string? FileStatus { get; set; }

        [Column("file_type", TypeName = "string")]
        public string? FileType { get; set; }

        [Column("mongo_db_object_id", TypeName = "string")]
        public string? MongoDbObjectId { get; set; }

        [Column("upload_date", TypeName = "datetime")]
        public DateTime? UploadDate { get; set; }

        [Column("process_date", TypeName = "datetime")]
        public DateTime? ProcessDate { get; set; }

        [Column("alias_name", TypeName = "string")]
        public string? Aliasname { get; set; }

        [Column("auth_users_id", TypeName = "int")]
        public int? AuthUserId { get; set; }
    }
}
