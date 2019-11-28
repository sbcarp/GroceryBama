using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GroceryBama.Entities
{
    [DataContract]
    public class Cart
    {
        public int Quantity { get; set; }
        public List<Item> Items { get; set; }

        public Cart()
        {
            Items = new List<Item>();
        }
    }

    public class CartParams
    {
        public int groceryId { get; set; }
        public int itemId { get; set; }
        public int quantity { get; set; }
    }
}
