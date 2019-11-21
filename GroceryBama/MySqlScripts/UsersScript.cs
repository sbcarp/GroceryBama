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
            if (!reader.HasRows) throw new UserCredentialNotMatchException("Incorrect username or password");
            User user = new User();
            user.Username = username;
            user.Firstname = reader.GetFieldValue<string>(0);
            user.Lastname = reader.GetFieldValue<string>(1);
            user.Role = reader.GetFieldValue<string>(2);
            if (reader.Read()) throw new MutipleUsersFoundException("Found more than one user");
            return user;
        }

    }
}
