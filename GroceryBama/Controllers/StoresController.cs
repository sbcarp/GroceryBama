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
            item.Id = 0;
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
            item.Id = 0;
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
            item.Id = 0;
            item.Group = "beverage";
            item.ListedPrice = 29.99;
            item.WholeSalePrice = 6.99;
            item.Quantity = 10;
            item.ExpirationDate = DateTime.Now.AddDays(30);
            item.Description = "Sweet Tea 10 packs";
            item.PictureUrl = "assets/images/sweet-tea.png";
            items.Add(item);


            return new JsonResult(items);
        }
    }
}