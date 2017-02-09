using System.IO;
using MessageHandler.EventProcessing.Runtime.Serialization;

namespace MessageHandler.EventProcessing.Runtime
{
    public class DefaultConvention:IConvention
    {
        private string _basePath;

        public DefaultConvention(string basePath="")
        {
            _basePath = basePath;
        }

        public void Apply(HandlerRuntimeConfiguration configuration)
        {
            var fullPath = Path.Combine(_basePath, "handler.runtime.json");
            using (var reader = File.OpenText(fullPath))
            {
                var json = reader.ReadToEndAsync().GetAwaiter().GetResult();
                dynamic deserialized =  Json.Decode(json);
                configuration.Runtime.HandlerInstanceID(deserialized.HandlerInstanceId);
                configuration.Runtime.HandlerConfigurationID(deserialized.HandlerConfigurationId);
                configuration.Runtime.AccountID(deserialized.AccountId);
                configuration.Runtime.EnvironmentID(deserialized.EnvironmentId);
                configuration.Runtime.ChannelID(deserialized.ChannelId);
                configuration.Runtime.TransportType(deserialized.TransportType);
                configuration.Runtime.ConnectionString(deserialized.Connectionstring);
            }
        }
    }
}
