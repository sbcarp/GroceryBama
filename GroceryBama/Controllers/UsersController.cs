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
        public User Login([FromBody]UserCredential userCredential)
        {
            User user;
            try
            {
                user = usersScript.GetUser(userCredential.Username, userCredential.Password);
            }
            catch (UserCredentialNotMatchException)
            {
                return null;
            }
            catch (MutipleUsersFoundException)
            {
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            
            

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
            user.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return user;
        }
        protected override void Dispose(bool disposing)
        {
            usersScript.Dispose();
            base.Dispose(disposing);
        }
    }
}
