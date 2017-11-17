using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime.Diagnostics
{
    public class TraceSettingsExtensionPoint: SettingsExtensionPoint
    {
        public TraceSettingsExtensionPoint(ISettings settings) : base(settings)
        {
        }
    }
}