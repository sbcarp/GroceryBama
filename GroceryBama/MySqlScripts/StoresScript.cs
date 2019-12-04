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
        public SearchResult GetItems(int? groceryId, int startIndex, int endIndex, string foodGroup)
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

        public Order Checkout(string username, int groceryId, DateTime requestDeliveryTime, string deliveryInstruction, int paymentMethodId)
        {
            MySqlDataReader reader = GetStoredProcedureReader("Checkout",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_groceryID", groceryId),
                            new MySqlParameter("@p_requestDeliveryTime", requestDeliveryTime),
                            new MySqlParameter("@p_deliveryInstruction", deliveryInstruction),
                            new MySqlParameter("@p_paymentMethodId", paymentMethodId)); 
            reader.Read();
            int newOrderId = (int)ReadColumn(reader, "t_newOrderID");
            reader.Close();
            return GetOrderDetail(username, newOrderId);
        }

        public SearchResult GetOrders(string username, int startIndex, int endIndex)
        {
            SearchResult searchResult = new SearchResult();
            searchResult.TotalNumberOfResults = 3;
            List<Order> orders = new List<Order>();
            MySqlDataReader reader = GetStoredProcedureReader("GetOrders",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_start", startIndex),
                            new MySqlParameter("@p_end", endIndex));
            reader.Read();
            searchResult.TotalNumberOfResults = (int)ReadColumn(reader, "OrderCount");
            reader.NextResult();
            while (reader.Read())
            {
                Order order = new Order();
                order.OrderId = (int)ReadColumn(reader, "OrderID");
                order.DelivererUsername = ReadColumn(reader, "DelivererUsername").ToString();
                order.DeliveryInstructions = ReadColumn(reader, "Instructions").ToString();
                order.Feedback = ReadColumn(reader, "Feedback").ToString();
                order.TotalPrice = (float)ReadColumn(reader, "TotalPrice");
                order.TotalItems = (int)ReadColumn(reader, "TotalItems");
                order.RequestDeliveryTime = (DateTime)ReadColumn(reader, "RequestDeliveryTime");
                order.Status = (int)ReadColumn(reader, "Status");
                order.BuyerUsername = ReadColumn(reader, "BuyerUsername").ToString();
                order.PaymentMethodId = (int)ReadColumn(reader, "PaymentMethodID");
                order.DateTime = (DateTime)ReadColumn(reader, "DateTime");
                order.StoreName = ReadColumn(reader, "StoreName").ToString();
                order.GroceryId = (int)ReadColumn(reader, "GroceryID");
                order.AddressLine2 = ReadColumn(reader, "AddressLine2").ToString();
                order.StreetAddress = ReadColumn(reader, "Street").ToString();
                order.City = ReadColumn(reader, "City").ToString();
                order.State = ReadColumn(reader, "State").ToString();
                order.ZipCode = ReadColumn(reader, "ZipCode").ToString();
                orders.Add(order);
            }
            reader.NextResult();
            foreach (Order order in orders)
            {
                List<Item> items = new List<Item>();
                while (reader.Read())
                {
                    Item item = new Item();
                    item.Id = (int)ReadColumn(reader, "ItemID");
                    item.Name = ReadColumn(reader, "ItemName").ToString();
                    item.Group = ReadColumn(reader, "FoodGroup").ToString();
                    item.ExpirationDate = (DateTime)ReadColumn(reader, "ExpDate");
                    item.Quantity = (int)ReadColumn(reader, "Quantity");
                    item.ListedPrice = (float)ReadColumn(reader, "PriceP");
                    item.WholeSalePrice = (float)ReadColumn(reader, "WholeSaleP");
                    item.Description = ReadColumn(reader, "Description").ToString();
                    item.PictureUrl = ReadColumn(reader, "PictureName").ToString();
                    items.Add(item);
                }
                order.Items = items;
                reader.NextResult();
            }
            searchResult.Results = orders;
            reader.Close();
            return searchResult;
        }
        public Order GetOrderDetail(string username, int orderId)
        {
            MySqlDataReader reader = GetStoredProcedureReader("GetOrder",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_orderID", orderId));
            reader.Read();
            Order order = new Order();
            order.OrderId = (int)ReadColumn(reader, "OrderID");
            order.DelivererUsername = ReadColumn(reader, "DelivererUsername").ToString();
            order.DeliveryInstructions = ReadColumn(reader, "Instructions").ToString();
            order.Feedback = ReadColumn(reader, "Feedback").ToString();
            order.TotalPrice = (float)ReadColumn(reader, "TotalPrice");
            order.TotalItems = (int)ReadColumn(reader, "TotalItems");
            order.RequestDeliveryTime = (DateTime)ReadColumn(reader, "RequestDeliveryTime");
            order.Status = (int)ReadColumn(reader, "Status");
            order.BuyerUsername = ReadColumn(reader, "BuyerUsername").ToString();
            order.PaymentMethodId = (int)ReadColumn(reader, "PaymentMethodID");
            order.DateTime = (DateTime)ReadColumn(reader, "DateTime");
            order.StoreName = ReadColumn(reader, "StoreName").ToString();
            order.GroceryId = (int)ReadColumn(reader, "GroceryID");
            order.AddressLine2 = ReadColumn(reader, "AddressLine2").ToString();
            order.StreetAddress = ReadColumn(reader, "Street").ToString();
            order.City = ReadColumn(reader, "City").ToString();
            order.State = ReadColumn(reader, "State").ToString();
            order.ZipCode = ReadColumn(reader, "ZipCode").ToString();

            reader.NextResult();
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                Item item = new Item();
                item.Id = (int)ReadColumn(reader, "ItemID");
                item.Name = ReadColumn(reader, "ItemName").ToString();
                item.Group = ReadColumn(reader, "FoodGroup").ToString();
                item.ExpirationDate = (DateTime)ReadColumn(reader, "ExpDate");
                item.Quantity = (int)ReadColumn(reader, "Quantity");
                item.ListedPrice = (float)ReadColumn(reader, "PriceP");
                item.WholeSalePrice = (float)ReadColumn(reader, "WholeSaleP");
                item.Description = ReadColumn(reader, "Description").ToString();
                item.PictureUrl = ReadColumn(reader, "PictureName").ToString();
                items.Add(item);
            }
            order.Items = items;
            reader.Close();
            return order;
        }
        public void UpdateOrderStatus(string username, int orderId, int newStatus)
        {
            MySqlDataReader reader = GetStoredProcedureReader("UpdateOrderStatus",
                            new MySqlParameter("@p_username", username),
                            new MySqlParameter("@p_orderID", orderId),
                            new MySqlParameter("@p_newStatus", newStatus));

            reader.Close();
        }

        public SearchResult GetOutstandingOrders(int groceryId, int startIndex, int endIndex)
        {
            MySqlDataReader reader = GetStoredProcedureReader("GetOutstandingOrders",
                            new MySqlParameter("@p_groceryID", groceryId),
                            new MySqlParameter("@p_start", startIndex),
                            new MySqlParameter("@p_end", endIndex));
            reader.Read();
            SearchResult searchResult = new SearchResult();
            searchResult.TotalNumberOfResults = (int)ReadColumn(reader, "OrderCount");
            reader.NextResult();
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                Order order = new Order();
                order.OrderId = (int)ReadColumn(reader, "OrderID");
                order.StoreName = ReadColumn(reader, "StoreName").ToString();
                order.RequestDeliveryTime = (DateTime)ReadColumn(reader, "RequestDeliveryTime");
                order.DateTime = (DateTime)ReadColumn(reader, "DateTime");
                order.TotalPrice = (float)ReadColumn(reader, "TotalPrice");
                order.TotalItems = (int)ReadColumn(reader, "TotalItems");
                orders.Add(order);
            }
            reader.NextResult();
            foreach (Order order in orders)
            {
                reader.Read();
                order.AddressLine2 = ReadColumn(reader, "AddressLine2").ToString();
                order.StreetAddress = ReadColumn(reader, "Street").ToString();
                order.City = ReadColumn(reader, "City").ToString();
                order.State = ReadColumn(reader, "State").ToString();
                order.ZipCode = ReadColumn(reader, "ZipCode").ToString();
            }
            reader.Close();
            searchResult.Results = orders;
            return searchResult;
        }

        public Statistic GetStatistic(int groceryId)
        {

            MySqlDataReader reader = GetStoredProcedureReader("GetStatistics",
                            new MySqlParameter("@p_groceryID", groceryId));
            reader.Read();
            Statistic statistic = new Statistic();
            statistic.StoreName = ReadColumn(reader, "StoreName").ToString();
            statistic.TotalItemsSold = (int)ReadColumn(reader, "TotalItemsSold");
            statistic.TotalProfit = (float)ReadColumn(reader, "TotalRevenue");
            reader.Close();
            return statistic;
        }
        public void UpdateInventoryItemQuantity(int groceryId, int quantity)
        {
            MySqlDataReader reader = GetStoredProcedureReader("UpdateInventoryItemQuantity",
                            new MySqlParameter("@p_itemID", groceryId),
                            new MySqlParameter("@p_Quantity", quantity));

            reader.Close();
        }
        public void DeleteItemFromInventory(int groceryId, int itemId)
        {

        }
    }
}
