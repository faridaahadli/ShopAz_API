using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopAZ_API.DBModels;
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
    //[Authorize]
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
        public async Task<IActionResult> Get()
        {
            //var userId = User.Claims.First(p => p.Type == "userId").Value;
            //int id = Convert.ToInt32(userId);
            var baskets = _mapper
               .Map<IEnumerable<Basket>, IEnumerable<BasketViewModel>>(_context.Baskets?
               .Where(p=>p.UserId==2)
               .Include(p => p.Product));         
            return Ok(baskets);
        }

        // GET api/<BasketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BasketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BasketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BasketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
