using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntimeSettings:SettingsExtensionPoint
    {
        public HandlerRuntimeSettings(ISettings settings) : base(settings)
        {
        }

        public void HandlerInstanceID(string id)
        {
            this.Settings.Set("HandlerInstanceID", id);
        }

        public void HandlerConfigurationID(string id)
        {
            this.Settings.Set("HandlerConfigurationID", id);
        }

        public void AccountID(string id)
        {
            this.Settings.Set("AccountID", id);
        }

        public void EnvironmentID(string id)
        {
            this.Settings.Set("EnvironmentID", id);
        }

        public void ChannelID(string id)
        {
            this.Settings.Set("ChannelID", id);
        }

        public void TransportType(string transportType)
        {
            this.Settings.Set("TransportType", transportType);
        }

        public void ConnectionString(string connectionString)
        {
            this.Settings.Set("ConnectionString", connectionString);
        }
    }
}