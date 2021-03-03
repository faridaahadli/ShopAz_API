using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual IEnumerable<ProductsOfOrder> Products { get; set; }

    }
}
