using MessageHandler.Runtime.Configuration;

namespace MessageHandler.Runtime
{
    public static class DynamicConfigurationSettingsExtensions { 

        public static void EnableDynamicConfiguration(this ConfigurationRoot configuration, IScriptEngine scriptEngine)
        {
            var container = configuration.GetContainer();
            container.Register<DynamicConfigurationSource>();
            container.Register(() => new InMemoryVariableSource(configuration.Settings.GetUserVariables()));
            container.Register(() => scriptEngine);
            container.Register<RegexTemplatingEngine>();
        }
    }
}