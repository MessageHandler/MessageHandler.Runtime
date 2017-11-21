using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ConfigurationSettingsExtensions
    {

        public static void HandlerInstanceId(this ConfigurationRoot configuration, string handlerInstanceId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<ConfigurationContext>();
            config.HandlerInstanceId = handlerInstanceId;
        }

        public static string GetHandlerInstanceId(this ISettings settings)
        {
            var config = settings.GetOrCreate<ConfigurationContext>();
            return config.HandlerInstanceId;
        }

        public static void HandlerConfigurationId(this ConfigurationRoot configuration,
            string handlerConfigurationId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<ConfigurationContext>();
            config.HandlerConfigurationId = handlerConfigurationId;
        }

        public static string GetHandlerConfigurationId(this ISettings settings)
        {
            var config = settings.GetOrCreate<ConfigurationContext>();
            return config.HandlerConfigurationId;
        }

        public static void AccountId(this ConfigurationRoot configuration, string accountId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<ConfigurationContext>();
            config.AccountId = accountId;
        }

        public static string GetAccountId(this ISettings settings)
        {
            var config = settings.GetOrCreate<ConfigurationContext>();
            return config.AccountId;
        }

        public static string GetProjectId(this ISettings settings)
        {
            var config = settings.GetOrCreate<ConfigurationContext>();
            return config.ProjectId;
        }

        public static void ProjectId(this ConfigurationRoot configuration, string projectId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<ConfigurationContext>();
            config.ProjectId = projectId;
        }

        public static void EnvironmentId(this ConfigurationRoot configuration, string environmentId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<ConfigurationContext>();
            config.EnvironmentId = environmentId;
        }

        public static string GetEnvironmentId(this ISettings settings)
        {
            var config = settings.GetOrCreate<ConfigurationContext>();
            return config.EnvironmentId;
        }

        public static void ChannelId(this ConfigurationRoot configuration, string channelId)
        {
            var settings = configuration.GetSettings();
            var config = settings.GetOrCreate<ConfigurationContext>();
            config.ChannelId = channelId;
        }

        public static string GetChannelId(this ISettings settings)
        {
            var config = settings.GetOrCreate<ConfigurationContext>();
            return config.ChannelId;
        }
    }
}