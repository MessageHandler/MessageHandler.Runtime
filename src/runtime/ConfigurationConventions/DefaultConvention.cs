﻿using System.Collections.Generic;
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

        public async Task Apply(HandlerRuntimeConfiguration configuration)
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
                configuration.TransportType((string)deserialized.TransportType);
                configuration.Connectionstring((string)deserialized.Connectionstring);
            }

            var fullPathConfig = Path.Combine(_basePath, "handler.config.json");
            using (var reader = File.OpenText(fullPathConfig))
            {
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                Dictionary<string, object> deserialized = Json.Decode(json);
                configuration.HandlerConfigurationValues(deserialized);
            }
        }
    }
}
