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
using Newtonsoft.Json.Linq;

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
        [HttpGet("GetStores")]
        public ActionResult GetStores()
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetStores()));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [HttpPost("SwitchStore")]
        public ActionResult SwitchStore([FromBody]int groceryId)
        {
            try
            {
                storesScript.SwitchStore(User.Identity.Name, groceryId);
                return Json(new BasePacket(true, null));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [AllowAnonymous]
        [HttpGet("GetItems")]
        public ActionResult GetItems(int groceryId, int startIndex, int endIndex, string foodGroup)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetItems(groceryId, startIndex, endIndex, foodGroup)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
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

        [Authorize(Roles = "buyer")]
        [HttpPost("AddToCart")]
        public JsonResult AddToCart([FromBody] CartParams Params)
        {
            try
            {
                int cartQuantity = storesScript.AddItemToCart(User.Identity.Name, Params.groceryId, Params.itemId, Params.quantity).Quantity;
                return Json(new BasePacket(true, new { cartQuantity = cartQuantity}));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }         
        }

        [Authorize(Roles = "buyer")]
        [HttpGet("GetCartQuantity")]
        public JsonResult GetCartQuantity()
        {
            var v = new { cartQuantity = 1 };
            return Json(new BasePacket(true, v));
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("RemoveItemFromCart")]
        public ActionResult RemoveItemFromCart([FromBody]CartParams Params)
        {
            try
            {
                int groceryId = Params.groceryId;
                int itemId = Params.itemId;
                return Json(new BasePacket(true, storesScript.RemoveItemFromCart(User.Identity.Name, groceryId, itemId)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("UpdateCartItemQuantity")]
        public JsonResult UpdateCartItemQuantity([FromBody] CartParams Params)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.UpdateCartItemQuantity(User.Identity.Name, Params.groceryId, Params.itemId, Params.quantity)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

    }
}