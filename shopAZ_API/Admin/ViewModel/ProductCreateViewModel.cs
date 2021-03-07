using shopAZ_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        public int StockCount { get; set; }
        public string StockCode { get; set; }
        public decimal? Price { get; set; }
        public string Seller { get; set; }
        public float Discount { get; set; } = 0;
        public bool? IsMoney { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<ProductInfoViewModel> ProductInfos { get; set; }
    }
}
