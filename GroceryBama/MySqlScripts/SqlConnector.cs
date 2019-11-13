using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GroceryBama.MySqlScripts
{
    public class SqlConnector : IDisposable
    {
        public MySqlConnection Connection; 
        public SqlConnector()
        {
            MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder();
            connectionString.Server = AppInformation.SqlServer;
            connectionString.Port = AppInformation.SqlPortNumber;
            connectionString.UserID = AppInformation.SqlUsername;
            connectionString.Password = AppInformation.SqlPassword;
            connectionString.Database = AppInformation.SqlDatabase;
            Connection = new MySqlConnection(connectionString.ToString());
            Connection.Open();
            Console.WriteLine("Connection Opened");
        }

        public void Dispose()
        {
            Connection.Close();
            Console.WriteLine("Connection Closed");
        }
    }
}
