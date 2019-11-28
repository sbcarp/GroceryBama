using System.Runtime.Serialization;

namespace GroceryBama.Entities
{
    [DataContract]
    public class SearchResult
    {
        public int TotalNumberOfResults { get; set; }
        public dynamic Results { get; set; }

    }
}
