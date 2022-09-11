using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Account
{
    public class Organization: BaseEntity
    {
        [Column("org_name", TypeName = "string")]
        public string OrgName { get; set; }
    }
}
