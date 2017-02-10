using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ContainerExtension
    {
        public static void UseContainer(this SettingsExtensionPoint extensionPoint,IContainer container)
        {
            extensionPoint.GetSettings().Set("messagehandler.container", container);
        }
    }
}