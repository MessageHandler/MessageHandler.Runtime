using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationConventions
{
    public class When_applying_default_convention
    {
        [Fact]
        public async Task Read_runtime_settings_from_file()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.RegisterConvention(new DefaultConvention("Configuration"));
            await HandlerRuntime.Create(config);
            Assert.Equal("1", settings.Get<HandlerRuntimeConfigurationValues>().HandlerInstanceId);
            Assert.Equal("1ed72b77-629d-45b8-bb0c-a6d4ceff92de", settings.Get<HandlerRuntimeConfigurationValues>().HandlerConfigurationId);
            Assert.Equal("messagehandler", settings.Get<HandlerRuntimeConfigurationValues>().AccountId);
            Assert.Equal("7d49577c-d64d-4926-bf71-de8212f10aab", settings.Get<HandlerRuntimeConfigurationValues>().EnvironmentId);
            Assert.Equal("727cce9e-f95f-468f-9437-1eb05105835d", settings.Get<HandlerRuntimeConfigurationValues>().ChannelId);
            Assert.Equal("EventHub", settings.Get<HandlerRuntimeConfigurationValues>().TransportType);
            Assert.Equal("Endpoint=sb://mynamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx=", settings.Get<HandlerRuntimeConfigurationValues>().Connectionstring);
        }
    }
}
