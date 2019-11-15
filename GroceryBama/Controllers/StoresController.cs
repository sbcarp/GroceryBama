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
        [AllowAnonymous]
        [HttpGet("getitems")]
        public JsonResult GetItems()
        {
            List<Item> items = new List<Item>();
            Item item = new Item();
            item.Name = "Sprite";
            item.Id = 1;
            item.Group = "beverage";
            item.ListedPrice = 9.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Sprite 10 packs";
            item.PictureUrl = "assets/images/sprite.png";
            items.Add(item);

            item = new Item();
            item.Name = "Fanta";
            item.Id = 2;
            item.Group = "beverage";
            item.ListedPrice = 9.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Fanta 10 packs";
            item.PictureUrl = "assets/images/fanta.jpg";
            items.Add(item);

            item = new Item();
            item.Name = "Sweet Tea";
            item.Id = 3;
            item.Group = "beverage";
            item.ListedPrice = 29.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Sweet Tea 10 packs";
            item.PictureUrl = "assets/images/sweet-tea.png";
            items.Add(item);

            item = new Item();
            item.Name = "SPAM Canned Meat";
            item.Id = 4;
            item.Group = "Canned Good";
            item.ListedPrice = 12.59;
            item.WholeSalePrice = 6.99;
            item.Quantity = 5;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Net weight 340g";
            item.PictureUrl = "assets/images/spam-canned-meat.jpg";
            items.Add(item);
            return Json(items);
        }
        [Authorize(Roles = "user")]
        [HttpGet("getcartitems")]
        public JsonResult GetCartItems()
        {
            Console.WriteLine(User.Identity.Name);
            List<Item> items = new List<Item>();
            Item item = new Item();
            item.Name = "Sprite";
            item.Id = 1;
            item.Group = "beverage";
            item.ListedPrice = 9.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Sprite 10 packs";
            item.PictureUrl = "assets/images/sprite.png";
            items.Add(item);

            item = new Item();
            item.Name = "Fanta";
            item.Id = 2;
            item.Group = "beverage";
            item.ListedPrice = 9.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Fanta 10 packs";
            item.PictureUrl = "assets/images/fanta.jpg";
            items.Add(item);

            item = new Item();
            item.Name = "Sweet Tea";
            item.Id = 3;
            item.Group = "beverage";
            item.ListedPrice = 29.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Sweet Tea 10 packs";
            item.PictureUrl = "assets/images/sweet-tea.png";
            items.Add(item);

            
            return Json(items);
        }

        [Authorize(Roles = "user")]
        [HttpPost("addtocart")]
        public JsonResult AddToCart([FromBody] ItemToCart item)
        {
            Console.WriteLine(item);
            var v = new { cartQuantity = 2 };
            return Json(v);
        }

        [Authorize(Roles = "user")]
        [HttpGet("getcartquantity")]
        public JsonResult GetCartQuantity()
        {
            var v = new { cartQuantity = 1 };
            return Json(v);
        }
    }
}