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
            cmd.CommandText = @"SELECT Ssn 
                                FROM EMPLOYEE
                                WHERE Fname=@firstname AND Lname=@lastname";
            cmd.Parameters.AddWithValue("@firstname", username);
            cmd.Parameters.AddWithValue("@lastname", password);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            // if no record return
            if (!reader.HasRows) throw new UserCredentialNotMatchException("Incorrect username or password");
            string ssn = reader.GetFieldValue<string>(0);
            if (reader.Read()) throw new MutipleUsersFoundException("Found more than one user");
            User user = new User(username, "admin");
            user.Firstname = ssn;
            user.Lastname = ssn;
            return user;
        }

    }
}
