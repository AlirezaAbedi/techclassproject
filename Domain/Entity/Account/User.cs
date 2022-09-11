using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Account
{
    public class User: BaseEntity
    {
        [Column("first_name", TypeName = "string")]
        public string? Firstname { get; set; }

        [Column("last_name", TypeName = "string")]
        public string? Lastname { get; set; }

        [Column("email", TypeName = "string")]
        public string? Email { get; set; }

        [Column("comment", TypeName = "string")]
        public string? Comment { get; set; }

        [Column("is_approved", TypeName = "tinyint")]
        public int isApproved { get; set; }

        [Column("date_created", TypeName = "datetime")]
        public DateTime dateCreated { get; set; }

        [Column("date_approved", TypeName = "datetime")]
        public DateTime dateApproved { get; set; }

        [Column("date_last_Modified", TypeName = "datetime")]
        public DateTime datelastModified { get; set; }

        [Column("auth_organizations_id", TypeName = "int")]
        public int authOrganizationsId { get; set; }


        [Column("auth_domain_id", TypeName = "int")]
        public int authDomainId { get; set; }

        [Column("auth_roles_id", TypeName = "int")]
        public int authRolesId { get; set; }


    }
}
