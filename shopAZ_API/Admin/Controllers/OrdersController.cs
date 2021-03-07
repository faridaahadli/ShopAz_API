using Admin.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopAZ_API.DBModels;
using shopAZ_API.Models;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get()
        {
            var list =_context.Orders?.Include(p=>p.Products)
                .ToList();
             
            return Ok(list);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _context.Orders?.Include(p=>p.Products)
                .ThenInclude(p=>p.Product)
                .FirstOrDefault(p => p.Id == id);
            if (order == null)
                return BadRequest();
            var viewOrder = _mapper.Map<SingleOrderViewModel>(order);
            viewOrder.Total = order.Products.Sum(p =>
              OrderAmount.GetTotal(p.Discount, (float)p.Product.Price, p.ProductCount, p.IsMoney));
            return Ok(viewOrder);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post()
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
