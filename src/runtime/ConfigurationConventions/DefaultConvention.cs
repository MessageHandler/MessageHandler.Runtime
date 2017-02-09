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
                configuration.HandlerInstanceId((string)deserialized.HandlerInstanceId);
                configuration.HandlerConfigurationId((string)deserialized.HandlerConfigurationId);
                configuration.AccountId((string)deserialized.AccountId);
                configuration.EnvironmentId((string)deserialized.EnvironmentId);
                configuration.ChannelId((string)deserialized.ChannelId);
                configuration.TransportType((string)deserialized.TransportType);
                configuration.Connectionstring((string)deserialized.Connectionstring);
            }
        }
    }
}
