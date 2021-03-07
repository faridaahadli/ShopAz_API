using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class Product
    {
        public int Id { get; set; }
        public int StockCount { get; set; }
        public string StockCode { get; set; }
        public decimal? Price { get; set; }
        public string Seller { get; set; }
        public int? ReserveCount { get; set; } = 0;
        public float Discount { get; set; } = 0;
        public bool IsMoney { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual IEnumerable<ProductInfoLang> ProductInfos { get; set; }
        public virtual IEnumerable<ProductsOfOrder> Products { get; set; }
        public virtual IEnumerable<Basket> Baskets{ get; set; }
    }
}
