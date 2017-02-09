using System;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntimeConfiguration:SettingsExtensionPoint
    {
        private readonly ISettings _settings;

        public HandlerRuntimeSettings Runtime { get; private set; }

        public HandlerRuntimeConfiguration():this(new Settings())
        {
        }

        public HandlerRuntimeConfiguration(ISettings settings):base(settings)
        {
            _settings = settings;
            Runtime = new HandlerRuntimeSettings(settings);
            ApplyDefaults();
        }

        private void ApplyDefaults()
        {
            _settings.SetDefault("messagehandler.container", new Container());
            _settings.SetDefault("messagehandler.shutdowngraceperiod", TimeSpan.FromSeconds(30));
        }

        internal void LockSettings()
        {
            _settings.LockAll();
        }
    }
}