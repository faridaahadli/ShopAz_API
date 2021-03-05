using Admin.ViewModel;
using AutoMapper;
using shopAZ_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductCreateViewModel,Product>();
            CreateMap<ProductInfoViewModel,ProductInfoLang>();
            CreateMap<Product,ProductCreateViewModel>();
            CreateMap<ProductInfoLang,ProductInfoViewModel>();
        }
    }
}
