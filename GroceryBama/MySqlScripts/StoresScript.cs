using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryBama.Entities;

namespace GroceryBama.MySqlScripts
{
    public class StoresScript : SqlConnector
    {
        public Cart GetCartItems(string username, int groceryID)
        {
            Cart cart = new Cart();
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
            cart.Items.Add(item);

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
            cart.Items.Add(item);

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
            cart.Items.Add(item);

            return cart;
        }

        public Cart AddItemToCart(string username, int groceryId, int itemId, int quantity)
        {
            Cart cart = new Cart();
            cart.Items = null;
            cart.Quantity = 4;
            return cart;
        }
    }
}
