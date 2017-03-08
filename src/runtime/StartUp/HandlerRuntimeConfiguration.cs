using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
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
            var container = new Container();
            container.Register(() => _settings);
            _settings.SetDefaultContainer(container);
            _settings.SetDefaultShutdownGracePeriod(TimeSpan.FromSeconds(30));
        }

        internal void LockSettings()
        {
            _settings.LockAll();
        }
    }
}