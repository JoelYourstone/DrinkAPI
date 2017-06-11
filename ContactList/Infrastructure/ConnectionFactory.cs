using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace DrinkAPI.Infrastructure
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.AppSettings.Get("SqlConnectionString"));
            connection.Open();
            return connection;
        }
    }
}