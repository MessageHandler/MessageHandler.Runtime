namespace MessageHandler.EventProcessing.Runtime.ConfigurationSettings
{
    public abstract class SettingsExtensionPoint
    {
        protected SettingsExtensionPoint(Settings settings)
        {
            Settings = settings;
        }

        internal Settings Settings { get; private set; }
    }
}