using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntimeConfiguration:SettingsExtensionPoint
    {
        private readonly ISettings _settings;

        public HandlerRuntimeConfiguration():this(new Settings())
        {
        }

        public HandlerRuntimeConfiguration(ISettings settings):base(settings)
        {
            _settings = settings;
        }

        public void LockSettings()
        {
            _settings.LockAll();
        }
    }
}