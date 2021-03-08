using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopAZ_API.DBModels;
using shopAZ_API.Validators;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopAZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BasketController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }
        // GET: api/<BasketController>
        [HttpGet]
        public IActionResult Get()
        {
            int usrId = Convert.ToInt32(User.Claims
                 .First(p => p.Type == "userId").Value);
            var baskets = _mapper
               .Map<IEnumerable<Basket>, IEnumerable<BasketViewModel>>(_context.Baskets?
               .Where(p=>p.UserId==usrId)
               .Include(p => p.Product));         
            return Ok(baskets);
        }

        // POST api/<BasketController>
        [HttpPost]
        public async Task<IActionResult> Post(BasketViewModel model)
        {
            int usrId = Convert.ToInt32(User.Claims
                 .First(p => p.Type == "userId").Value);
            BasketValidator validator = new BasketValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
                return BadRequest("Model is not valid");
            var product =  _context.Products?.Include(p => p.ProductInfos)
                .FirstOrDefault(p => p.Id == model.ProductId);
            if (product == null)
                return NotFound();
            var basket = _mapper.Map<Basket>(model);
            //Use current user id
            basket.UserId = usrId;
             _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<BasketController>/5
        [HttpPut]
        public async Task<IActionResult> Put(BasketViewModel model)
        {
            BasketValidator validator = new BasketValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
                return BadRequest("Model is not valid");
            var basket = _context.Baskets?
                .FirstOrDefault(p =>p.Id==model.Id);
            if (basket == null)
                return NotFound();
            //Error with mapper
            //basket = _mapper.Map<Basket>(model);
            basket.ProductCount = model.ProductCount;
            basket.ProductId = model.ProductId;
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        // DELETE api/<BasketController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var basket= _context.Baskets
                .FirstOrDefault(p => p.Id == id);
            if (basket == null)
                return NotFound();
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
