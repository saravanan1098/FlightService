using AuthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;
        private UserDbontext db;
        public LoginController(IConfiguration _config, UserDbontext _db)
        {
            config = _config;
            db = _db;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User request)
        {
            db.Users.Add(request);
            db.SaveChanges();
            return Ok(request);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto userLogin)
        {
            var user = Authenticate(userLogin);
            if(user!=null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        private string Generate(User user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]

            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token =new JwtSecurityToken(config["Jwt:Issuer"], 
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserDto userLogin)
        {
            var currentUser = db.Users.FirstOrDefault(o => o.Username.ToLower() ==
                userLogin.Username.ToLower() && o.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    
    }
}
