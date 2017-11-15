using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MessageHandler.Runtime.Serialization;

namespace MessageHandler.Runtime
{
    public class DefaultConvention:IConvention
    {
        private string _basePath;

        public DefaultConvention(string basePath="")
        {
            _basePath = basePath;
        }

        public async Task Apply(ConfigurationRoot configuration)
        {
            var fullPath = Path.Combine(_basePath, "handler.runtime.json");
            using (var reader = File.OpenText(fullPath))
            {
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                dynamic deserialized =  Json.Decode(json);
                configuration.HandlerInstanceId((string)deserialized.HandlerInstanceId);
                configuration.HandlerConfigurationId((string)deserialized.HandlerConfigurationId);
                configuration.AccountId((string)deserialized.AccountId);
                configuration.EnvironmentId((string)deserialized.EnvironmentId);
                configuration.ChannelId((string)deserialized.ChannelId);
            }

            var fullPathConfig = Path.Combine(_basePath, "handler.config.json");
            using (var reader = File.OpenText(fullPathConfig))
            {
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                var deserialized = Json.Decode<Dictionary<string, object>>(json);
                configuration.HandlerConfigurationValues(deserialized);
            }

            var fullPathVariables = Path.Combine(_basePath, "handler.variables.json");
            using (var reader = File.OpenText(fullPathVariables))
            {
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                var deserialized = Json.Decode<List<Variable>>(json);
                configuration.UserVariables(deserialized);
            }
            
        }
    }
}
