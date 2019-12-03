using System.Collections.Generic;

namespace GroceryBama.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ConfirmationCode { get; set; }
        public int GroceryId { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public int? DefaultPaymentMethodId { get; set; }
    }

    public class UserCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
