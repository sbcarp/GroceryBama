using System.Runtime.Serialization;

namespace GroceryBama.Entities
{
    [DataContract]
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Hours { get; set; }
    }
}
