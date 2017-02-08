using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public static class ContainerExtension
    {
        public static void UseContainer(this SettingsExtensionPoint extensionPoint,IContainer container)
        {
            extensionPoint.GetSettings().Set("messagehandler.container", container);
        }
    }
}