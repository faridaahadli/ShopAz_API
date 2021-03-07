using Admin.ViewModel;
using AutoMapper;
using shopAZ_API.DBModels;
using shopAZ_API.ViewModels;
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
            CreateMap<ProdOrder,SingleOrderViewModel>();
            CreateMap<SingleOrderViewModel,ProdOrder> ();
            CreateMap<ProductsOfOrder,PrdOfOrderViewModel>();
            //Shop_AZAPI project
            CreateMap<BasketViewModel, Basket>();
            CreateMap<Basket, BasketViewModel>();
            CreateMap<OrderViewModel, ProdOrder>();
            CreateMap<ProdOrder,OrderViewModel>();
            CreateMap<PrdOfOrderViewModel,ProductsOfOrder>();
            CreateMap<Basket, PrdOfOrderViewModel>();
        }
    }
}
