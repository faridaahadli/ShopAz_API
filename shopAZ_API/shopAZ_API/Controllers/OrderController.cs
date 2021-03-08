using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopAZ_API.DBModels;
using shopAZ_API.Enums;
using shopAZ_API.Helpers;
using shopAZ_API.Interfaces;
using shopAZ_API.Models;
using shopAZ_API.Validators;
using shopAZ_API.ViewModels;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopAZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
      
        public OrderController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

        // POST api/<OrderController>
        //function for basket
        [HttpPost]
        public async Task<IActionResult> Post(CardData model)
        {
            int usrId = Convert.ToInt32(User.Claims
                  .First(p => p.Type == "userId").Value);
            var rvdPrd=_context.Products.ToList();
            CardValidator validator = new CardValidator();
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
                return BadRequest(result.Errors);
            decimal? amount=0;
            var basket = _context.Baskets
                 .Where(p => p.UserId == usrId)
                 .Include(p => p.Product);

            //stripe uses cent we multiply 100  
           
            foreach(var element in basket)
            {

                amount +=OrderAmount.GetTotal(element.Product.Discount,
                    (float)element.Product.Price, element.ProductCount, element.Product.IsMoney);
            }
            amount *= 100;
            if (amount<50)
                return BadRequest("Payment amount should be at least 50 cent");
            IPayment payment = new Payment();
            var success = payment.MakePayment(model, (long)amount);
            if (!await success)
            {
                return BadRequest();
            }
           
            //Stripe has an Order class
            var order = new OrderViewModel();
            order.CreateDate = DateTime.Now;
            order.Status = (int)OrderStatus.Status.pending;
            order.UserId = usrId;
            order.Products = _mapper.Map<IEnumerable<Basket>,IEnumerable<PrdOfOrderViewModel>>(basket);
            var dbOrder = _mapper.Map<ProdOrder>(order);
            foreach(var prd in dbOrder.Products)
            {
                var search= rvdPrd.FirstOrDefault(p => p.Id == prd.ProductId);
                prd.Discount = search.Discount;
                prd.IsMoney = search.IsMoney;
            }
            await _context.Orders.AddAsync(dbOrder);
            await _context.SaveChangesAsync();
            _context.Baskets.RemoveRange(basket);
            foreach (var element in basket)
            {
               var reservedPrd = rvdPrd.FirstOrDefault(p => p.Id == element.ProductId);
                int reserveCount = (int)reservedPrd.ReserveCount;
                reserveCount += element.ProductCount;
                reservedPrd.ReserveCount = reserveCount;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Function for buy one product
        [HttpPost]
        [Route("{id}/{count}")]
        public async Task<IActionResult> Post(int id,CardData model, int count = 1)
        {
            int usrId = Convert.ToInt32(User.Claims
                 .First(p => p.Type == "userId").Value);
            var rvdPrd = _context.Products.ToList();
            CardValidator validator = new CardValidator();
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
                return BadRequest(result.Errors);
            decimal? amount;
            var product = rvdPrd.FirstOrDefault(p=>p.Id==id);
            //stripe uses cent we multiply 100  
            amount= OrderAmount.GetTotal(product.Discount,
                   (float)product.Price, count, product.IsMoney);
            amount *= 100;
            if (amount < 50)
                return BadRequest("Payment amount should be at least 50 cent");
            IPayment payment = new Payment();
            var success = payment.MakePayment(model, (long)amount);
            if (!await success)
            {
                return BadRequest();
            }

            //Stripe has an Order class
            var prdOfOrder = new PrdOfOrderViewModel();
            var list = new List<PrdOfOrderViewModel>();
            list.Add(prdOfOrder);
            prdOfOrder.ProductId = product.Id;
            prdOfOrder.ProductCount = count;
            var order = new OrderViewModel();
            order.CreateDate = DateTime.Now;
            order.Status = (int)OrderStatus.Status.pending;
            order.UserId = usrId;
            order.Products = list;
            var dbOrder = _mapper.Map<ProdOrder>(order);
            foreach (var prd in dbOrder.Products)
            {
                var search = rvdPrd.FirstOrDefault(p => p.Id == prd.ProductId);
                prd.Discount = search.Discount;
                prd.IsMoney = search.IsMoney;
            }
            await _context.Orders.AddAsync(dbOrder);
            await _context.SaveChangesAsync();
            int reserveCount = (int)product.ReserveCount;
            reserveCount += count;
            product.ReserveCount = reserveCount;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
