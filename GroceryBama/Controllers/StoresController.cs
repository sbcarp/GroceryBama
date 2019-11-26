using System;
using System.Collections.Generic;
using System.Text.Json;
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

namespace GroceryBama.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StoresController : Controller
    {
        private StoresScript storesScript;

        public StoresController()
        {
            storesScript = new StoresScript();
        }
        [AllowAnonymous]
        [HttpGet("GetItems")]
        public ActionResult GetItems()
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetCartItems(User.Identity.Name, 0).Items));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize(Roles = "buyer,deliverer")]
        [HttpGet("GetCartItems")]
        public JsonResult GetCartItems()
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetCartItems(User.Identity.Name, 0).Items));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer,deliverer")]
        [HttpPost("AddToCart")]
        public JsonResult AddToCart([FromBody] ItemToCart item)
        {
            try
            {
                int cartQuantity = storesScript.AddItemToCart(User.Identity.Name, 0, 0, 5).Quantity;
                return Json(new BasePacket(true, new { cartQuantity = cartQuantity}));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }         
        }

        [Authorize(Roles = "buyer,deliverer")]
        [HttpGet("GetCartQuantity")]
        public JsonResult GetCartQuantity()
        {
            var v = new { cartQuantity = 1 };
            return Json(new BasePacket(true, v));
        }
    }
}