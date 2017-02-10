using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.StartUp
{
    public class When_extending_handler_runtime_configuration
    {
        [Fact]
        public void Can_extend_settings()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.MySetting("key", "value");
            Assert.Equal(settings.Get("key"), "value");
        }   
    }

    public static class When_extending_handler_runtime_configuration_MyExtension
    {
        public static void MySetting(this SettingsExtensionPoint extensionPoint, string key, string value)
        {
            extensionPoint.GetSettings().Set("key", "value");
        }
    }
}
