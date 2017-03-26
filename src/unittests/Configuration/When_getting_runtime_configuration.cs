using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
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
            Assert.Equal("value", settings.GetHandlerInstanceId());
        }

        [Fact]
        public void Can_set_handler_configurationID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.HandlerConfigurationId("value");
            Assert.Equal("value", settings.GetHandlerConfigurationId());
        }

        [Fact]
        public void Can_set_handler_accountID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.AccountId("value");
            Assert.Equal("value", settings.GetAccountId());
        }

        [Fact]
        public void Can_set_handler_environmentID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.EnvironmentId("value");
            Assert.Equal("value", settings.GetEnvironmentId());
        }

        [Fact]
        public void Can_set_handler_channelID()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.ChannelId("value");
            Assert.Equal("value", settings.GetChannelId());
        }
    }
}
