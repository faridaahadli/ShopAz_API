using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class Lang
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public virtual IEnumerable<ProductInfoLang> ProductInfos { get; set; }
    }
}
