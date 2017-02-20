using System;
using System.Threading.Tasks;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class HandlerRuntimeConfigurationSettingsExtensions
    {
        private const string MessageHandlerPipelineKey = "MessageHandler.Pipeline";
        public static void HandlerInstanceId(this HandlerRuntimeConfiguration configuration, string handlerInstanceId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.HandlerInstanceId = handlerInstanceId;
        }

        public static string GetHandlerInstanceId(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.HandlerInstanceId;
        }

        public static void HandlerConfigurationId(this HandlerRuntimeConfiguration configuration,
            string handlerConfigurationId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.HandlerConfigurationId = handlerConfigurationId;
        }

        public static string GetHandlerConfigurationId(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.HandlerConfigurationId;
        }

        public static void AccountId(this HandlerRuntimeConfiguration configuration, string accountId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.AccountId = accountId;
        }

        public static string GetAccountId(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.AccountId;
        }

        public static void EnvironmentId(this HandlerRuntimeConfiguration configuration, string environmentId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.EnvironmentId = environmentId;
        }

        public static string GetEnvironmentId(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.EnvironmentId;
        }

        public static void ChannelId(this HandlerRuntimeConfiguration configuration, string channelId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.ChannelId = channelId;
        }

        public static string GetChannelId(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.ChannelId;
        }

        public static void TransportType(this HandlerRuntimeConfiguration configuration, string transportType)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.TransportType = transportType;
        }

        public static string GetTransportType(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.TransportType;
        }

        public static void Connectionstring(this HandlerRuntimeConfiguration configuration, string connectionString)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.Connectionstring = connectionString;
        }

        public static string GetConnectionstring(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.Connectionstring;
        }
        public static void Pipeline<T>(this HandlerRuntimeConfiguration configuration, Func<T, Task> pipeline)
        {
            var settings = configuration.GetSettings();
            settings.Set(MessageHandlerPipelineKey, pipeline);
        }

        public static Func<T, Task> GetPipeline<T>(this ISettings settings)
        {
            return settings.Get<Func<T, Task>>(MessageHandlerPipelineKey);
        }
    }
}