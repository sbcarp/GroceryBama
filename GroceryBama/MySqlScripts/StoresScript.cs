using GroceryBama.Entities;
using System;
using System.Collections.Generic;

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
            cart.Quantity++;
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

        public Order Checkout(string username, int groceryId, string requestDeliveryTime, string deliveryInstruction, int paymentMethodId)
        {
            return new Order();
        }

        public SearchResult GetOrders(string username, int startIndex, int endIndex)
        {
            SearchResult searchResult = new SearchResult();
            searchResult.TotalNumberOfResults = 3;
            List<Order> orders = new List<Order>();
            Order order = new Order();
            order.OrderId = 1;
            order.DateTime = new DateTime(2019, 11, 27);
            order.DeliveryInstructions = "I'm tired of making up data";
            order.GroceryId = 1;
            order.Items = GetCartItems(username, order.GroceryId).Items;
            order.RequestDeliveryTime = "ASAP";
            order.Status = 0;
            order.StoreName = "Publix";
            order.TotalItems = 30;
            order.TotalPrice = 56.33;
            orders.Add(order);

            order = new Order();
            order.OrderId = 2;
            order.DateTime = new DateTime(2019, 11, 26);
            order.DeliveryInstructions = "I'm tired of making up data";
            order.GroceryId = 1;
            order.Items = GetCartItems(username, order.GroceryId).Items;
            order.RequestDeliveryTime = "In 2 Hours";
            order.Status = 1;
            order.StoreName = "Walmart";
            order.TotalItems = 30;
            order.TotalPrice = 56.33;
            orders.Add(order);

            order = new Order();
            order.OrderId = 3;
            order.DateTime = new DateTime(2019, 11, 25);
            order.DeliveryInstructions = "I'm tired of making up data";
            order.GroceryId = 1;
            order.Items = GetCartItems(username, order.GroceryId).Items;
            order.RequestDeliveryTime = "ASAP";
            order.Status = 2;
            order.StoreName = "Somewhere";
            order.TotalItems = 30;
            order.TotalPrice = 56.33;
            orders.Add(order);

            searchResult.Results = orders;
            return searchResult;
        }
        public Order GetOrderDetail(string username, int orderId)
        {
            Order order = new Order();

            order.OrderId = 1;
            order.GroceryId = 1;
            order.DateTime = new DateTime(2019, 11, 27);
            order.DeliveryInstructions = "vcvsdagsad";
            order.Items = GetItems(order.GroceryId, 1, 10, "").Results;
            order.PaymentMethodId = 1;
            order.RequestDeliveryTime = "ASAP";
            order.Status = 0;
            order.StoreName = "Publix";
            order.TotalItems = 55;
            order.TotalPrice = 66.44;
            order.AddressLine2 = "apt 122";
            order.StreetAddress = "555 15th St";
            return order;
        }
        public void UpdateOrderStatus(string username, int orderId, int newStatus)
        {
            
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
