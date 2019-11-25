using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using GroceryBama.MySqlScripts;
using GroceryBama.Entities;
using GroceryBama.Exceptions;
using MySql.Data.MySqlClient;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryBama.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private UsersScript usersScript;
        public UsersController()
        {
            usersScript = new UsersScript();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public JsonResult Login([FromBody]UserCredential userCredential)
        {
            try
            {
                User user = usersScript.GetUser(userCredential.Username, userCredential.Password);
                user.Token = GenerateToken(user);
                return Json(new BasePacket(true, user));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        private string GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppInformation.Key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
        protected override void Dispose(bool disposing)
        {
            usersScript.Dispose();
            base.Dispose(disposing);
        }
    }
}
