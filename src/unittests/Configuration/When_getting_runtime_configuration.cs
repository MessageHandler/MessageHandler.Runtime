using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.Configuration
{
    public class When_getting_runtime_configuration
    {
        [Fact]
        public void Can_set_handler_instanceID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.HandlerInstanceId("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().HandlerInstanceId);
        }

        [Fact]
        public void Can_set_handler_configurationID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.HandlerConfigurationId("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().HandlerConfigurationId);
        }

        [Fact]
        public void Can_set_handler_accountID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.AccountId("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().AccountId);
        }

        [Fact]
        public void Can_set_handler_environmentID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.EnvironmentId("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().EnvironmentId);
        }

        [Fact]
        public void Can_set_handler_channelID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.ChannelId("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().ChannelId);
        }

        [Fact]
        public void Can_set_handler_transportType()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.TransportType("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().TransportType);
        }

        [Fact]
        public void Can_set_handler_connectionString()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Connectionstring("value");
            Assert.Equal("value", settings.Get<HandlerRuntimeConfigurationValues>().Connectionstring);
        }
    }
}
