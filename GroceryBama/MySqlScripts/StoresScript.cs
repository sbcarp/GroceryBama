using GroceryBama.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GroceryBama.MySqlScripts
{
    public class StoresScript : SqlConnector
    {
        public Cart GetCartItems(string username, int groceryId)
        {
            MySqlDataReader reader = GetStoredProcedureReader("GetCart",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_groceryID", groceryId));
            reader.Read();
            Cart cart = new Cart();

            cart.Quantity = (int)ReadColumn(reader, "Quantity");

            reader.NextResult();
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                Item item = new Item();
                item.Description = ReadColumn(reader, "Description").ToString();
                item.ExpirationDate = (DateTime)ReadColumn(reader, "ExpDate");
                item.Group = ReadColumn(reader, "FoodGroup").ToString();
                item.Id = (int)ReadColumn(reader, "ItemID");
                item.ListedPrice = (float)ReadColumn(reader, "PriceP");
                item.Name = ReadColumn(reader, "ItemName").ToString();
                item.PictureUrl = ReadColumn(reader, "PictureName").ToString();
                item.Quantity = (int)ReadColumn(reader, "CartQuantity");
                item.WholeSalePrice = (float)ReadColumn(reader, "WholeSaleP");
                items.Add(item);
            }
            cart.Items = items;
            reader.Close();
            return cart;
        }
        public int GetCartQuantity(string username, int groceryId)
        {
            MySqlDataReader reader = GetStoredProcedureReader("GetCartQuantity",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_groceryID", groceryId));
            reader.Read();
            int cartQuantity = (int)ReadColumn(reader, "Quantity");
            reader.Close();
            return cartQuantity;
        }
        public int AddItemToCart(string username, int groceryId, int itemId, int quantity)
        {
            MySqlDataReader reader = GetStoredProcedureReader("AddItemToCart",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_itemId", itemId),
                            new MySqlParameter("@p_groceryID", groceryId),
                            new MySqlParameter("@p_quantity", quantity));
            reader.Read();
            int cartQuantity = (int)ReadColumn(reader, "Quantity");
            reader.Close();
            return cartQuantity;
        }
        public Cart RemoveItemFromCart(string username, int groceryId, int itemId)
        {
            MySqlDataReader reader = GetStoredProcedureReader("RemoveItemFromCart",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_itemId", itemId),
                            new MySqlParameter("@p_groceryID", groceryId));
            reader.Close();
            return GetCartItems(username, groceryId);
        }
        public Cart UpdateCartItemQuantity(string username, int groceryId, int itemId, int newQuantity)
        {
            MySqlDataReader reader = GetStoredProcedureReader("UpdateItemInCart",
                        new MySqlParameter("@p_username", username),
                        new MySqlParameter("@p_itemId", itemId),
                        new MySqlParameter("@p_groceryID", groceryId),
                        new MySqlParameter("@p_newQuantity", newQuantity));
            reader.Close();
            return GetCartItems(username, groceryId);
        }
        public List<Store> GetStores()
        {
            MySqlDataReader reader = GetStoredProcedureReader("GetStores");

            List<Store> stores = new List<Store>();
            while (reader.Read())
            {
                Store store = new Store();
                store.Address = ReadColumn(reader, "Street").ToString() + ", " + ReadColumn(reader, "City").ToString() + ", " + ReadColumn(reader, "State").ToString();
                store.Hours = ReadColumn(reader, "OpenHour").ToString() + " to " + ReadColumn(reader, "CloseHour").ToString();
                store.Id = (int)ReadColumn(reader, "GroceryID");
                store.Name = ReadColumn(reader, "StoreName").ToString();
                store.PhoneNumber = ReadColumn(reader, "Phone").ToString();
                stores.Add(store);
            }
            reader.Close();
            return stores;
        }
        public void SwitchStore(string username, int newGroceryId)
        {

        }
        public SearchResult GetItems(int groceryId, int startIndex, int endIndex, string foodGroup)
        {
            MySqlDataReader reader = GetStoredProcedureReader("SearchItems",
                            new MySqlParameter("@p_groceryID", groceryId),
                            new MySqlParameter("@p_start", startIndex),
                            new MySqlParameter("@p_end", endIndex),
                            new MySqlParameter("@p_foodGroup", foodGroup));
            reader.Read();
            SearchResult searchResult = new SearchResult();

            searchResult.TotalNumberOfResults = (int)ReadColumn(reader, "ItemCount");

            reader.NextResult();
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                Item item = new Item();
                item.Description = ReadColumn(reader, "Description").ToString();
                item.ExpirationDate =(DateTime)ReadColumn(reader, "ExpDate");
                item.Group = ReadColumn(reader, "FoodGroup").ToString();
                item.Id = (int)ReadColumn(reader, "ItemID");
                item.ListedPrice = (float)ReadColumn(reader, "PriceP");
                item.Name = ReadColumn(reader, "ItemName").ToString();
                item.PictureUrl = ReadColumn(reader, "PictureName").ToString();
                item.Quantity = (int)ReadColumn(reader, "Quantity");
                item.WholeSalePrice = (float)ReadColumn(reader, "WholeSaleP");
                items.Add(item);
            }
            searchResult.Results = items;
            reader.Close();
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
            order.AddressLine2 = "aaafdsafasdfsa";
            order.StreetAddress = "123 Ave";
            order.City = "Tuscaloosa";
            order.State = "AL";
            order.ZipCode = "35487";
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
            order.AddressLine2 = "aaafdsafasdfsa";
            order.StreetAddress = "123 Ave";
            order.City = "Tuscaloosa";
            order.State = "AL";
            order.ZipCode = "35487";
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
            order.AddressLine2 = "aaafdsafasdfsa";
            order.StreetAddress = "123 Ave";
            order.City = "Tuscaloosa";
            order.State = "AL";
            order.ZipCode = "35487";
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
            return GetOrders("",0,0);
        }

        public Statistic GetStatistic(int groceryId)
        {
            Statistic statistic = new Statistic();
            if (groceryId == 1)
            {
                statistic.GroceryId = groceryId;
                statistic.StoreName = "Publix";
                statistic.TotalItemsSold = 5500;
                statistic.TotalProfit = (float)657446.45;
            }
            else
            {
                statistic.GroceryId = groceryId;
                statistic.StoreName = "Walmart";
                statistic.TotalItemsSold = 6666;
                statistic.TotalProfit = (float)5574613.01;
            }
            return statistic;
        }
        public void AddItemToInventory(int groceryId, int itemId, int quantity)
        {
            
        }
        public void DeleteItemFromInventory(int groceryId, int itemId)
        {

        }
    }
}
