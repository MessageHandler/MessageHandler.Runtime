using MessageHandler.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationSettings
{
    public class When_setting_values
    {
        [Fact]
        public void Can_set_value()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            Assert.Equal(settings.Get("key"), "value");
        }

        [Fact]
        public void Can_set_default_value()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value");
            Assert.Equal(settings.GetDefault("key"), "value");
        }

        [Fact]
        public void Can_get_default_value()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value");
            Assert.Equal(settings.Get("key"), "value");
        }

        [Fact]
        public void Explicit_value_overrules_default_value()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value1");
            settings.Set("key", "value2");
            Assert.Equal(settings.Get("key"), "value2");
        }
    }
}
