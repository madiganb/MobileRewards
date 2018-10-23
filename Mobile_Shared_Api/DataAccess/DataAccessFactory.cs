using System;
using System.Configuration;

namespace Mobile_Shared_Api.DataAccess
{
    public static class DataAccessFactory
    {
        public static BaseDataLayer GetDataLayer(string connectionString, string providerName)
        {
            if (providerName.Equals("system.data.sqlclient", StringComparison.OrdinalIgnoreCase))
            {
                return new SqlDataLayer(connectionString);
            }

            //Leave room for other implementations

            //Default Implementation
            return new SqlDataLayer(connectionString);
        }

        public static BaseDataLayer GetDataLayer(ConnectionStringSettings connectionStringSettings)
        {
            if (connectionStringSettings.ProviderName.Equals("system.data.sqlclient", StringComparison.OrdinalIgnoreCase))
            {
                return new SqlDataLayer(connectionStringSettings.ConnectionString);
            }

            //Leave room for other implementations

            //Default Implementation
            return new SqlDataLayer(connectionStringSettings.ConnectionString);
        }
    }
}