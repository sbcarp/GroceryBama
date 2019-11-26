using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryBama.Entities;
using MySql.Data.MySqlClient;
using GroceryBama.Exceptions;
using System.Data;

namespace GroceryBama.MySqlScripts
{
    public class UsersScript : SqlConnector
    {
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
            User user = new User();

            return user;
        }
        public User AddPaymentMethod(string username, string name, string accountNumber, string routineNumber, bool isDefault)
        {
            User user = new User();

            return user;
        }
        public User UpdatePaymentMethod(string username, int paymentMethodId, string name, string accountNumber, 
                                        string routineNumber, bool isDefault)
        {
            User user = new User();

            return user;
        }
        public User DeletePaymentMethod(string username, int paymentMethodId)
        {
            User user = new User();

            return user;
        }
        public void SwitchStore(string username, int groceryId)
        {

        }
    }
}
