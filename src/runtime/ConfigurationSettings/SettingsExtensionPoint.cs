namespace MessageHandler.Runtime.ConfigurationSettings
{
    public abstract class SettingsExtensionPoint
    {
        protected SettingsExtensionPoint(ISettings settings)
        {
            Settings = settings;
        }

        internal ISettings Settings { get; private set; }
    }
}