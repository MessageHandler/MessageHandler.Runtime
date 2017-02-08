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

            ApplyDefaults();
        }

        private void ApplyDefaults()
        {
            _settings.SetDefault("messagehandler.container", new Container());
        }

        public void LockSettings()
        {
            _settings.LockAll();
        }
    }
}