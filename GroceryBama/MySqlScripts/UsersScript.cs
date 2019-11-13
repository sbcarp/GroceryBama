using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryBama.Entities;
using MySql.Data.MySqlClient;
using GroceryBama.Exceptions;

namespace GroceryBama.MySqlScripts
{
    public class UsersScript : SqlConnector
    {
        public User GetUser(string username, string password)
        {
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = @"SELECT fname, lname, role 
                                FROM userexample
                                WHERE username=@username AND password=@password";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            // if no record return
            if (!reader.HasRows) throw new UserCredentialNotMatchException("Incorrect username or password");
            string firstname = reader.GetFieldValue<string>(0);
            string lastname = reader.GetFieldValue<string>(1);
            string role = reader.GetFieldValue<string>(2);
            if (reader.Read()) throw new MutipleUsersFoundException("Found more than one user");
            User user = new User(username, "admin");
            user.Firstname = firstname;
            user.Lastname = lastname;
            user.Role = role;
            return user;
        }

    }
}
