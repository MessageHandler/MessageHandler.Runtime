using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class HandlerRuntimeConfigurationSettingsExtensions
    {
        public static void HandlerInstanceId(this HandlerRuntimeConfiguration configuration, string handlerInstanceId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.HandlerInstanceId = handlerInstanceId;
        }

        public static void HandlerConfigurationId(this HandlerRuntimeConfiguration configuration,
            string handlerConfigurationId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.HandlerConfigurationId = handlerConfigurationId;
        }

        public static void AccountId(this HandlerRuntimeConfiguration configuration, string accountId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.AccountId = accountId;
        }

        public static void EnvironmentId(this HandlerRuntimeConfiguration configuration, string environmentId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.EnvironmentId = environmentId;
        }

        public static void ChannelId(this HandlerRuntimeConfiguration configuration, string channelId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.ChannelId = channelId;
        }

        public static void TransportType(this HandlerRuntimeConfiguration configuration, string transportType)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.TransportType = transportType;
        }

        public static void Connectionstring(this HandlerRuntimeConfiguration configuration, string connectionString)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.Connectionstring = connectionString;
        }
    }
}