using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GroceryBama.Entities
{
    [DataContract]
    public class SearchResult
    {
        public int TotalNumberOfResults { get; set; }
        public dynamic Results { get; set; }

    }
}
