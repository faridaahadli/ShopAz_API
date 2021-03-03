using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class ProductInfoLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int LangId { get; set; }
        public virtual Lang Lang { get; set; }
    }
}
