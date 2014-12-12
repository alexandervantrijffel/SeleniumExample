using System;
using System.Configuration;

namespace Structura.GuiTests.Utilities
{
    public static class ConfigurationHelper
    {
        public static T Get<T>(string name)
        {
            var value = ConfigurationManager.AppSettings[name];
            Guard.IsNotNull<InvalidOperationException>(value, "AppSetting with name {0} not found. Please check the application configuration file.", name);
            if (typeof(T).IsEnum)
                return (T) Enum.Parse(typeof (T), value);
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static string GetConnectionString(string name)
        {
            var value = ConfigurationManager.ConnectionStrings[name];
            Guard.IsNotNull<InvalidOperationException>(value, "ConnectionString with name {0} not found. Please check the application configuration file.", name);
            return value.ConnectionString;
        }
    }
}
