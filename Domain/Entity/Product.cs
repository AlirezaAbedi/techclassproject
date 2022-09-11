using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Product //: BaseEntity
    {
        //public string Name { get; set; }
        //public string Barcode { get; set; }
        //public string Description { get; set; }
        //public decimal Rate { get; set; }


        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
    }
}
