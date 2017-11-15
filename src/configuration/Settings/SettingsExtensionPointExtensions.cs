namespace MessageHandler.Runtime.ConfigurationSettings
{
    public static class SettingsExtensionPointExtensions
    {
        public static ISettings GetSettings(this SettingsExtensionPoint extensionPoint)
        {
            return extensionPoint.Settings;
        }
    }
}