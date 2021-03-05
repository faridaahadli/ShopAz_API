using shopAZ_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    public class ProductInfoViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LangId { get; set; }
    }
}
