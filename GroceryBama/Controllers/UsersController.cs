using GroceryBama.Entities;
using GroceryBama.MySqlScripts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        [HttpPost("Login")]
        public JsonResult Login([FromBody]UserCredential userCredential)
        {
            try
            {
                User user = usersScript.GetUser(userCredential.Username.Trim(), userCredential.Password);
                user.Token = GenerateToken(user);
                return Json(new BasePacket(true, user));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public JsonResult Register([FromBody]User newUser)
        {
            try
            {
                Console.WriteLine(newUser);
                User user = null;
                if (newUser.Role == "buyer") user = usersScript.RegisterBuyer(newUser);
                else if (newUser.Role == "deliverer") user = usersScript.RegisterDeliverer(newUser);
                else if (newUser.Role == "manager") user = usersScript.RegisterManager(newUser);
                user.Username = newUser.Username;
                user.Password = newUser.Password;
                return Json(new BasePacket(true, user));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize(Roles = "buyer")]
        [HttpGet("GetUserPaymentMethods")]
        public JsonResult GetUserPaymentMethods()
        {
            try
            {
                User user = usersScript.GetUserInfo(User.Identity.Name);
                List<PaymentMethod> paymentMethods = user.PaymentMethods;
                var returnData = new { defaultPaymentMethodId = user.DefaultPaymentMethodId, paymentMethods = paymentMethods };
                return Json(new BasePacket(true, returnData));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize(Roles = "buyer")]
        [HttpPost("UpdatePaymentMethod")]
        public JsonResult UpdatePaymentMethod([FromBody]PaymentMethod paymentMethod)
        {
            try
            {
                User user = usersScript.UpdatePaymentMethod(User.Identity.Name, paymentMethod.Id, paymentMethod.Name, paymentMethod.AccountNumber, paymentMethod.RoutineNumber, paymentMethod.IsDefault);
                List<PaymentMethod> paymentMethods = user.PaymentMethods;
                var returnData = new { defaultPaymentMethodId = user.DefaultPaymentMethodId, paymentMethods = paymentMethods };
                return Json(new BasePacket(true, returnData));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize(Roles = "buyer")]
        [HttpPost("AddPaymentMethod")]
        public JsonResult AddPaymentMethod([FromBody]PaymentMethod paymentMethod)
        {
            try
            {
                User user = usersScript.AddPaymentMethod(User.Identity.Name, paymentMethod.Name, paymentMethod.AccountNumber, paymentMethod.RoutineNumber, paymentMethod.IsDefault);
                List<PaymentMethod> paymentMethods = user.PaymentMethods;
                var returnData = new { defaultPaymentMethodId = user.DefaultPaymentMethodId, paymentMethods = paymentMethods };
                return Json(new BasePacket(true, returnData));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("DeletePaymentMethod")]
        public JsonResult DeletePaymentMethod([FromBody]int paymentMethodId)
        {
            try
            {
                User user = usersScript.DeletePaymentMethod(User.Identity.Name, paymentMethodId);
                List<PaymentMethod> paymentMethods = user.PaymentMethods;
                var returnData = new { defaultPaymentMethodId = user.DefaultPaymentMethodId, paymentMethods = paymentMethods };
                return Json(new BasePacket(true, returnData));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpGet("GetUserAddress")]
        public JsonResult GetUserAddress()
        {
            try
            {
                User user = usersScript.GetUserInfo(User.Identity.Name);
                var returnData = new
                {
                    streetAddress = user.StreetAddress,
                    addressLine2 = user.AddressLine2,
                    city = user.City,
                    state = user.State,
                    zipCode = user.ZipCode
                };
                return Json(new BasePacket(true, returnData));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize]
        [HttpGet("GetUserContact")]
        public JsonResult GetUserContact()
        {
            try
            {
                User user = usersScript.GetUserInfo(User.Identity.Name);
                var returnData = new
                {
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                };
                return Json(new BasePacket(true, returnData));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("UpdateUserAddress")]
        public JsonResult UpdateUserAddress([FromBody]User user)
        {
            try
            {
                user = usersScript.UpdateUserAddressInfo(User.Identity.Name, user.StreetAddress, user.AddressLine2, user.City, user.State, user.ZipCode);
                return Json(new BasePacket(true, user));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize]
        [HttpPost("UpdateUserContact")]
        public JsonResult UpdateUserContact([FromBody]User user)
        {
            try
            {
                user = usersScript.UpdateUserContact(User.Identity.Name, user.PhoneNumber, user.Email);
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
