using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public class ConfigurationRoot : SettingsExtensionPoint
    {
        private readonly ISettings _settings;

        public ConfigurationRoot() : this(new Settings())
        {
        }

        public ConfigurationRoot(ISettings settings) : base(settings)
        {
            _settings = settings;
        }

        public void LockSettings()
        {
            _settings.LockAll();
        }
    }
}