using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GroceryBama.Entities
{
    [DataContract]
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public double ListedPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
    }
    public class ItemToCart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
