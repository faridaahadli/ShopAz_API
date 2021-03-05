using CryptoHelper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shopAZ_API.DBModels;
using shopAZ_API.Models;
using shopAZ_API.Validators;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopAZ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //Register function for users
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            RegisterValidator validator = new RegisterValidator();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                return BadRequest();
            }
            var user = new User();
            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = Crypto.HashPassword(model.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var userRole = new UserRolePivot();
            userRole.UserId = user.Id;
            userRole.RoleId = _context.Roles.FirstOrDefault(p => p.Name.Equals("customer")).Id;
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            LoginValidator validator = new LoginValidator();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                return BadRequest();
            }
            var user = await _context.Users?
                .FirstOrDefaultAsync(p => p.Username == model.Username 
                && p.Password== "AQAAAAEAACcQAAAAEEK+BdU+QbEH1ObS2zdKiG2PC1Zz8V0BzZOMWDrZKwgz6ktVH5Si0Wn67QdIGGpFAw=="); //"AQAAAAEAACcQAAAAEEK+BdU+QbEH1ObS2zdKiG2PC1Zz8V0BzZOMWDrZKwgz6ktVH5Si0Wn67QdIGGpFAw=="
            if (user == null)
                return Unauthorized();
            var userprofile = new UserProfile();
            userprofile.Username = user.Username;
            userprofile.Token = CreateToken(user); 
            return Ok(userprofile.Token);
        }

        public static string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("userId",user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescripter = new SecurityTokenDescriptor()
            {
                
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                //Issuer = "http://localhost:52870",
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }

    }
}
