namespace MessageHandler.Runtime.ConfigurationSettings
{
    public abstract class SettingsExtensionPoint
    {
        protected SettingsExtensionPoint(ISettings settings)
        {
            Settings = settings;
        }

        public ISettings Settings { get; private set; }
    }
}