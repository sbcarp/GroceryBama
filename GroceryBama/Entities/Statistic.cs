using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryBama.Entities
{
    public class Statistic
    {
        // A statistic within 1 month period
        public double TotalProfit { get; set; }
        public int TotalItemsSold { get; set; }
    }
}
