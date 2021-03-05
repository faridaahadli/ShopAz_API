using Admin.Validators;
using Admin.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopAZ_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductCreateViewModel>>(_context.Products
                .Include(p=>p.ProductInfos));
            return Ok(products);
        }

        // GET api/<ProductController>/5
         [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products?.Include(p=>p.ProductInfos)?
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();          
            var productview = _mapper.Map<ProductCreateViewModel>(product);
            return Ok(productview);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateViewModel model)
        {
            ProductValidator validator = new ProductValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(model);
            //product.Price = model.Price;
            //product.StockCode = model.StockCode;
            //product.StockCount = model.StockCount;
            //product.Discount = model.Discount;
            //product.IsMoney = model.IsMoney;
            //product.StartDate = model.StartDate;
            //product.EndDate = model.EndDate;
            //product.Seller = model.Seller;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public IActionResult Put(ProductCreateViewModel model)
        {
            ProductValidator validator = new ProductValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest("Model is not valid");
            }
            var product = _context.Products.FirstOrDefault(p => p.Id == model.Id);
            if (product == null)
                return NotFound("Product doesn't exist");
            product = _mapper.Map<Product>(model);
            _context.Products.Update(product);
            return Ok(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            var product = _context.Products?.Include(p=>p.ProductInfos)?
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product doesn't exist");
          _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
