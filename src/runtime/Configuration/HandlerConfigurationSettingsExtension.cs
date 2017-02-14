﻿using System.Collections.Generic;
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

        internal static Dictionary<string, object> GetHandlerConfigurationValues(this Settings settings)
        {
            return settings.Get<Dictionary<string, object>>(handlerConfigurationValuesKey);
        }

        public static void HandlerConfigurationRouting(this HandlerRuntimeConfiguration configuration, HandlerRouting routing)
        {
            var settings = configuration.GetSettings();
            settings.Set(handlerConfigurationRoutingKey, routing);
        }

        internal static HandlerRouting GetHandlerConfigurationRouting(this Settings settings)
        {
            return settings.Get<HandlerRouting>(handlerConfigurationRoutingKey);
        }
    }
}
