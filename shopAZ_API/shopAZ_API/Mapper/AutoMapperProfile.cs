using AutoMapper;
using shopAZ_API.DBModels;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BasketViewModel, Basket>();
            CreateMap<Basket,BasketViewModel>();
            CreateMap<OrderViewModel, ProdOrder>();
            CreateMap<PrdOfOrderViewModel, ProductsOfOrder>();
            CreateMap<Basket,PrdOfOrderViewModel>();
        }
    }
}
