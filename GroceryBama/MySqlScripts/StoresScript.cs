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
            Cart cart = GetCartItems(username, groceryId);
            cart.Quantity ++;
            return cart;
        }
        public Cart RemoveItemFromCart(string username, int groceryId, int itemId)
        {
            Cart cart = GetCartItems(username, groceryId);
            cart.Items.RemoveAt(0);
            cart.Quantity--;
            return cart;
        }
        public Cart UpdateCartItemQuantity(string username, int groceryId, int itemId, int newQuantity)
        {
            Cart cart = GetCartItems(username, groceryId);
            cart.Items[0].Quantity = 222;
            return cart;
        }
        public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            Store store = new Store();
            store.Id = 1;
            store.Name = "Publix";
            store.Address = "1190 University Blvd";
            store.PhoneNumber = "(205)391-1204";
            store.Hours = "9AM to 10PM";
            stores.Add(store);

            store = new Store();
            store.Id = 2;
            store.Name = "Walmart";
            store.Address = "1501 Skyland Blvd E";
            store.PhoneNumber = "(205)391-1204";
            store.Hours = "7/24 Hours";
            stores.Add(store);
            return stores;
        }
        public void SwitchStore(string username, int newGroceryId)
        {

        }
        public SearchResult GetItems(int groceryId, int startIndex, int endIndex, string foodGroup)
        {
            SearchResult searchResult = new SearchResult();
            searchResult.Results = GetCartItems("sadfaioweuyrapweoiu", groceryId).Items;
            searchResult.TotalNumberOfResults = 55;
            return searchResult;
        }

        public Order Checkout(string username, int groceryId, string deliveryTime, string deliveryInstruction, int paymentMethodId)
        {
            return new Order();
        }

        public SearchResult GetOrders(string username, int startIndex, int endIndex)
        {
            SearchResult searchResult = new SearchResult();

            return searchResult;
        }

        public Order UpdateOrderStatus(string username, int orderId, string newStatus)
        {
            return new Order();
        }

        public SearchResult GetOutstandingOrders(int groceryId, int startIndex, int endIndex)
        {
            SearchResult searchResult = new SearchResult();

            return searchResult;
        }

        public Statistic GetStatistic(int groceryId)
        {
            return new Statistic();
        }

        public void DeleteItemFromInventory(int groceryId, int itemId)
        {

        }
    }
}
