using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GroceryBama.Entities
{
    [DataContract]
    public class Order
    {

        public int OrderId { get; set; }
        public int GroceryId { get; set; }
        public string BuyerUsername { get; set; }
        public string DelivererUsername { get; set; }
        public int PaymentMethodId { get; set; }
        public string StoreName { get; set; }
        public string DeliveryInstructions { get; set; }
        public string Feedback { get; set; }
        public float TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public DateTime RequestDeliveryTime { get; set; }
        public int RequestDeliveryTimeOffsetInHours { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public List<Item> Items { get; set; }

        public Order()
        {
            Items = new List<Item>();
        }
    }
}
