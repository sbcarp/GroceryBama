using GroceryBama.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GroceryBama.MySqlScripts
{
    public class UsersScript : SqlConnector
    {
        public User RegisterBuyer(User user)
        {
            return new User();
        }
        public User RegisterDeliverer(User user)
        {
            return new User();
        }
        public User RegisterManager(User user)
        {
            return new User();
        }
        public User GetUser(string username, string password)
        {
            MySqlCommand cmd = new MySqlCommand("login", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_username", username);
            cmd.Parameters.AddWithValue("@p_password", password);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            User user = new User();
            user.Username = reader.GetValue(reader.GetOrdinal("username")).ToString();
            user.Firstname = reader.GetValue(reader.GetOrdinal("fname")).ToString();
            user.Lastname = reader.GetValue(reader.GetOrdinal("lname")).ToString();
            user.Role = reader.GetValue(reader.GetOrdinal("role")).ToString();
            return user;
        }

        public User GetUserInfo(string username)
        {
            User user = new User();
            user.AddressLine2 = "Building 302";
            user.City = "Tuscaloosa";
            user.DefaultPaymentMethodId = 1;
            user.Email = "odsijf@gmail.com";
            user.Firstname = "Snow";
            user.GroceryId = 2;
            user.Lastname = "Jhon";
            user.PaymentMethods = new List<PaymentMethod>();
            user.PhoneNumber = "2058874645";
            user.Role = "buyer";
            user.State = "AL";
            user.StreetAddress = "444 14th Ave";
            user.Username = username;
            user.ZipCode = "35487";
            PaymentMethod paymentMethod = new PaymentMethod();
            paymentMethod.Id = 1;
            paymentMethod.Name = "Wells Fargo";
            paymentMethod.AccountNumber = "44216496794616";
            paymentMethod.RoutineNumber = "7979797";
            user.PaymentMethods.Add(paymentMethod);
            paymentMethod = new PaymentMethod();
            paymentMethod.Id = 2;
            paymentMethod.Name = "Credit Union";
            paymentMethod.AccountNumber = "1967941979794";
            paymentMethod.RoutineNumber = "659897";
            user.PaymentMethods.Add(paymentMethod);
            PaymentMethod defaultPaymentMethod = user.PaymentMethods.Find(item => { return item.Id == user.DefaultPaymentMethodId; });
            if (defaultPaymentMethod != null) defaultPaymentMethod.IsDefault = true;
            return user;
        }

        public User UpdateUserContactInfo(string username, string phoneNumber, string email)
        {
            User user = new User();

            return user;
        }

        public User UpdateUserAddressInfo(string username, string streetAddress, string addressLine2,
                                            string city, string state, string zipCode)
        {
            User user = GetUserInfo(username);
            return user;
        }
        public User UpdateUserContact(string username, string phoneNumber, string email)
        {
            User user = GetUserInfo(username);
            return user;
        }

        public User AddPaymentMethod(string username, string name, string accountNumber, string routineNumber, bool isDefault)
        {
            User user = GetUserInfo(username);
            PaymentMethod paymentMethod = new PaymentMethod();
            paymentMethod.Id = 3;
            paymentMethod.Name = name;
            paymentMethod.AccountNumber = accountNumber;
            paymentMethod.RoutineNumber = routineNumber;
            user.PaymentMethods.Add(paymentMethod);
            return user;
        }
        public User UpdatePaymentMethod(string username, int paymentMethodId, string name, string accountNumber,
                                        string routineNumber, bool isDefault)
        {
            User user = GetUserInfo(username);
            return user;
        }
        public User DeletePaymentMethod(string username, int paymentMethodId)
        {
            User user = GetUserInfo(username);
            user.PaymentMethods.Remove(user.PaymentMethods.Last());
            return user;
        }
        public void SwitchStore(string username, int groceryId)
        {

        }
    }
}
