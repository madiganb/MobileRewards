using System.Configuration;

namespace Mobile_Shared_Api
{
    public static class Settings
    {
        public static ConnectionStringSettings LocalDbConnectionStringSettings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["LocalDb"];
            }
        }

        public static string LocalDbConnectionString
        {
            get
            {
                return LocalDbConnectionStringSettings.ConnectionString;
            }
        }

        public static string LocalDbProviderName
        {
            get
            {
                return LocalDbConnectionStringSettings.ProviderName;
            }
        }

        public static int PersistanceIntervalMS
        {
            get
            {
                var interval = 0;

                if (!int.TryParse(ConfigurationManager.AppSettings["PersistanceIntervalMS"], out interval))
                {
                    return 60000;
                }

                return interval;
            }
        }
    }
}