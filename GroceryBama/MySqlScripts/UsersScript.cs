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

    }
}
