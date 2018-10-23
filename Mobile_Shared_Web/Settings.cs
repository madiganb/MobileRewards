using System.Configuration;

namespace Mobile_Shared_Web
{
    public static class Settings
    {
        public static string BaseUrl { get { return ConfigurationManager.AppSettings["BaseUrl"]; } }
    }
}