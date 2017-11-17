using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public class MetricsExtensionPoint : SettingsExtensionPoint
    {
        public MetricsExtensionPoint(ISettings settings) : base(settings)
        {
        }
    }
}