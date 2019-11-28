namespace GroceryBama.Entities
{
    public class Statistic
    {
        // A statistic within 1 month period
        public int GroceryId { get; set; }
        public string StoreName { get; set; }
        public double TotalProfit { get; set; }
        public int TotalItemsSold { get; set; }
    }
}
