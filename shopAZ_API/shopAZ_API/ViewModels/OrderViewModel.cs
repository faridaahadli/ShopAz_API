using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.ViewModels
{
    public class OrderViewModel
    {
        public int Status { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<PrdOfOrderViewModel> Products { get; set; }
    }
}
