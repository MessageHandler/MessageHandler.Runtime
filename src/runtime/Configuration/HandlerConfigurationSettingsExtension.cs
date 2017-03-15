using System.Collections.Generic;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class HandlerConfigurationSettingsExtension
    {
        private const string handlerConfigurationValuesKey = "messagehandler.handlerconfigurationvalues";
        private const string handlerConfigurationRoutingKey = "messagehandler.handlerconfigurationrouting";

        public static void HandlerConfigurationValues(this HandlerRuntimeConfiguration configuration, Dictionary<string, object> values)
        {
            var settings = configuration.GetSettings();
            settings.Set(handlerConfigurationValuesKey, values);
        }

        public static Dictionary<string, object> GetHandlerConfigurationValues(this ISettings settings)
        {
            return settings.Get<Dictionary<string, object>>(handlerConfigurationValuesKey);
        }

        public static void HandlerRoutingConfiguration(this HandlerRuntimeConfiguration configuration, HandlerRouting routing)
        {
            var settings = configuration.GetSettings();
            settings.Set(handlerConfigurationRoutingKey, routing);
        }

        public static HandlerRouting GetHandlerRoutingConfiguration(this ISettings settings)
        {
            return settings.Get<HandlerRouting>(handlerConfigurationRoutingKey);
        }
    }
}
