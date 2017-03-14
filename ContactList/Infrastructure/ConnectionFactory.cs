using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactList.Infrastructure
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