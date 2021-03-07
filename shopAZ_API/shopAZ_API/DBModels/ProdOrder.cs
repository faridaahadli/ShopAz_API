using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class ProdOrder
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual IEnumerable<ProductsOfOrder> Products { get; set; }

    }
}
