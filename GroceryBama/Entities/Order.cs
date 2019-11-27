using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GroceryBama.Entities
{
    [DataContract]
    public class Order
    {

        public int OrderId { get; set; }
        public int GroceryId { get; set; }
        public int paymentMethodId { get; set; }
        public string StoreName { get; set; }
        public string DeliveryInstructions { get; set; }
        public string Feedback { get; set; }
        public float TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public string DateTime { get; set; }
        public List<Item> Items { get; set; }
        public string Status { get; set; }
        public string RequestDeliveryTime { get; set; }
        public Order()
        {
            Items = new List<Item>();
        }
    }
}
