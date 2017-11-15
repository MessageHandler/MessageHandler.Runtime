using MessageHandler.Runtime.Configuration;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class HandlerRuntimeConfigurationSettingsExtensions
    {

        public static void HandlerInstanceId(this ConfigurationRoot configuration, string handlerInstanceId)
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

        public static void HandlerConfigurationId(this ConfigurationRoot configuration,
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

        public static void AccountId(this ConfigurationRoot configuration, string accountId)
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

        public static string GetProjectId(this ISettings settings)
        {
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            return config.ProjectId;
        }

        public static void ProjectId(this ConfigurationRoot configuration, string projectId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<HandlerRuntimeConfigurationValues>();
            config.ProjectId = projectId;
        }

        public static void EnvironmentId(this ConfigurationRoot configuration, string environmentId)
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

        public static void ChannelId(this ConfigurationRoot configuration, string channelId)
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

        public static void EnableDynamicConfiguration(this ConfigurationRoot configuration)
        {
            var container = configuration.GetContainer();
            container.Register<DynamicConfigurationSource>();
            container.Register<VariableSource>();
            container.Register<RoslynScriptingEngine>();
            container.Register<RoslynRegexTemplatingEngine>();
        }
    }
}