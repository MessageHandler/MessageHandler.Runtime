using System.Collections.Generic;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class HandlerConfigurationSettingsExtension
    {
        private const string handlerConfigurationValuesKey = "messagehandler.handlerconfigurationvalues";

        public static void HandlerConfigurationValues(this ConfigurationRoot configuration, Dictionary<string, object> values)
        {
            var settings = configuration.GetSettings();
            settings.Set(handlerConfigurationValuesKey, values);
        }

        public static Dictionary<string, object> GetHandlerConfigurationValues(this ISettings settings)
        {
            return settings.GetOrCreate<Dictionary<string, object>>(handlerConfigurationValuesKey);
        }
    }
}
