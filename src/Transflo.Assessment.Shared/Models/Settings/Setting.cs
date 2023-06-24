using Microsoft.Extensions.Configuration;

namespace Transflo.Assessment.Shared.Models.Settings
{
    public static class Setting
    {
        private static string applicationConnectionString = null!;
        public static string ApplicationConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(applicationConnectionString))
                {
                    applicationConnectionString = GetConnectionString("ApplicationConnectionString");
                }

                return applicationConnectionString;
            }
        }
        private static IConfiguration _configuration;
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration ??= configuration;
        }
        public static T Get<T>(string key)
        {
            return _configuration.GetValue<T>(key);
        }
        private static string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }

        public static T GetSection<T>(string key)
        {
            return _configuration.GetSection(key).Get<T>();
        }
    }
}
