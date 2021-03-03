using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class Basket
    {
        public int Id { get; set; }
        public int ProductCount { get; set; }
        public int ProductId { get; set; }
        public virtual  Product Product { get; set; }
        public int UserId{ get; set; }
        public virtual User User { get; set; }
        
    }
}
