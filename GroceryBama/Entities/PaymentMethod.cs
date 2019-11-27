using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GroceryBama.Entities
{
    [DataContract]
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string RoutineNumber { get; set; }
        public bool IsDefault { get; set; }
    }
}
