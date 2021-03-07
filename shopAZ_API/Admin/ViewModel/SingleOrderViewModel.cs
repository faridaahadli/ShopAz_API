using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    public class SingleOrderViewModel
    {
        public int Status { get; set; }
        public int UserId { get; set; }
        public decimal? Total { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<PrdOfOrderViewModel> Products { get; set; } = new List<PrdOfOrderViewModel>();
    }
}
