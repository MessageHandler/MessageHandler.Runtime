namespace MessageHandler.EventProcessing.Runtime.ConfigurationSettings
{
    public static class SettingsExtensionPointExtensions
    {
        public static Settings GetSettings(this SettingsExtensionPoint extensionPoint)
        {
            return extensionPoint.Settings;
        }
    }
}