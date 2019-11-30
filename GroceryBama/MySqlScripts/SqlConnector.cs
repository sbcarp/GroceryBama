using MySql.Data.MySqlClient;
using System;
using System.Data;

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
        public MySqlDataReader GetStoredProcedureReader(string procedureName, params MySqlParameter[] mySqlParameters)
        {
            MySqlCommand cmd = new MySqlCommand(procedureName, Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(mySqlParameters);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public object ReadColumn(MySqlDataReader reader, string fieldName)
        {
            return reader.GetValue(reader.GetOrdinal(fieldName));
        }
        public void Dispose()
        {
            Connection.Close();
            Console.WriteLine("Connection Closed");
        }
    }
}
