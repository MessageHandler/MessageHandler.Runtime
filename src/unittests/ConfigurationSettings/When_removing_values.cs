using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationSettings
{
    public class When_removing_values
    {
        [Fact]
        public void Can_remove_value()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.Remove("key");
            Assert.Null(settings.Get("key"));
        }
        [Fact]
        public void Can_remove_default_value()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value");
            settings.Remove("key");
            Assert.Null(settings.Get("key"));
        }

        [Fact]
        public void Remove_clears_lock()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.Lock("key");
            settings.Remove("key");
            settings.Set("key", "value2");
            Assert.Equal(settings.Get("key"), "value2");
        }

        [Fact]
        public void Can_clear_all_settings()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.Set("key2", "value");
            settings.Clear();
            Assert.Null(settings.Get("key"));
            Assert.Null(settings.Get("key2"));
        }
        [Fact]
        public void Can_clear_all_default_settings()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value");
            settings.SetDefault("key2", "value");
            settings.Clear();
            Assert.Null(settings.Get("key"));
            Assert.Null(settings.Get("key2"));
        }
    }
}
