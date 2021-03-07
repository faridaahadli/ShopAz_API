using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class ProductsOfOrder
    {
        public int Id { get; set; }
        public int ProductCount { get; set; }
        public float Discount { get; set; }
        public bool IsMoney { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId{ get; set; }
        public virtual ProdOrder Order { get; set; }
        

    }
}
