using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ContainerExtension
    {
        private const string ContainerKey = "messagehandler.container";

        public static void UseContainer(this ConfigurationRoot configuration,IContainer container)
        {
            var settings = configuration.GetSettings();
            container.Register(() => settings);
            settings.Set(ContainerKey, container);
        }

        public static IContainer GetContainer(this ConfigurationRoot configuration)
        {
            return configuration.GetSettings().GetContainer();
        }

        public static IContainer GetContainer(this ISettings settings)
        {
            return settings.Get<IContainer>(ContainerKey);
        }

        public static void SetDefaultContainer(this ISettings settings, IContainer container)
        {
            settings.SetDefault(ContainerKey, container);
        }
    }
}