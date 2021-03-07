using AutoMapper;
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
using Product = shopAZ_API.DBModels.Product;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopAZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post(CardData model)
        {
           
            var rvdPrd=_context.Products.ToList();
            CardValidator validator = new CardValidator();
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
                return BadRequest(result.Errors);
            decimal? amount;
            var basket = _context.Baskets
                 .Where(p => p.UserId == 2)
                 .Include(p => p.Product);
            //stripe uses cent we multiply 100
            
            amount = basket.Sum(p => p.Product.Price * p.ProductCount)*100;
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
            order.UserId = 2;
            order.Products = _mapper.Map<IEnumerable<Basket>, IEnumerable<PrdOfOrderViewModel>>(basket);
            var dbOrder = _mapper.Map<ProdOrder>(order);
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
      
        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //public decimal getTotalCountBasket(int userId)
        //{
           
        //}
    }
}
