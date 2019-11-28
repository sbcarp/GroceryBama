using System;
using System.Runtime.Serialization;

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
    public class ItemParams
    {
        public int ItemId { get; set; }
        public int GroceryId { get; set; }
        public int Quantity { get; set; }
    }
}
