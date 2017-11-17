using System.Collections.Generic;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ConfigurationValuesExtensions
    {
        private const string handlerConfigurationValuesKey = "messagehandler.configurationvalues";

        public static void ConfigurationValues(this ConfigurationRoot configuration, Dictionary<string, object> values)
        {
            var settings = configuration.GetSettings();
            settings.Set(handlerConfigurationValuesKey, values);
        }

        public static Dictionary<string, object> GetConfigurationValues(this ISettings settings)
        {
            return settings.GetOrCreate<Dictionary<string, object>>(handlerConfigurationValuesKey);
        }
    }
}
