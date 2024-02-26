using clutch_identity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace clutch_identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private IConfiguration config;
        public UserController(IConfiguration config)
        {
            this.config = config; 
        }



        [HttpPost]
        public async Task<string> Login(Users request)
        {
           var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, request.Email),
                new(JwtRegisteredClaimNames.Email, request.Email),
                new(ClaimTypes.Role, "User"),
                new("userId", request.Id.ToString())
            };
            var Sectoken = new JwtSecurityToken(config["JWTSettings:Issuer"],
              config["JWTSettings:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials); ;

            var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }


      
    }
}
