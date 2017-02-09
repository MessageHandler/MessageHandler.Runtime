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
            config.Runtime.HandlerInstanceID("value");
            Assert.Equal("value", settings.Get("HandlerInstanceID"));
        }

        [Fact]
        public void Can_set_handler_configurationID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Runtime.HandlerConfigurationID("value");
            Assert.Equal("value", settings.Get("HandlerConfigurationID"));
        }

        [Fact]
        public void Can_set_handler_accountID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Runtime.AccountID("value");
            Assert.Equal("value", settings.Get("AccountID"));
        }

        [Fact]
        public void Can_set_handler_environmentID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Runtime.EnvironmentID("value");
            Assert.Equal("value", settings.Get("EnvironmentID"));
        }

        [Fact]
        public void Can_set_handler_channelID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Runtime.ChannelID("value");
            Assert.Equal("value", settings.Get("ChannelID"));
        }

        [Fact]
        public void Can_set_handler_transportType()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Runtime.TransportType("value");
            Assert.Equal("value", settings.Get("TransportType"));
        }

        [Fact]
        public void Can_set_handler_connectionString()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.Runtime.ConnectionString("value");
            Assert.Equal("value", settings.Get("ConnectionString"));
        }
    }
}
