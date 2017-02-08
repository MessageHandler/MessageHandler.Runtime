using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationSettings
{
    public class When_locking_values
    {
        [Fact]
        public void Can_lock_individual_setting()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.Lock("key");
            Assert.Throws<SettingLockedException>(() => settings.Set("key", "value2"));
        }

        [Fact]
        public void Can_lock_individual_default_setting()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value");
            settings.Lock("key");
            Assert.Throws<SettingLockedException>(() => settings.SetDefault("key", "value2"));
        }

        [Fact]
        public void Can_lock_all_settings()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.Set("key2", "value2");
            settings.LockAll();
            Assert.Throws<SettingLockedException>(() => settings.Set("key", "value3"));
            Assert.Throws<SettingLockedException>(() => settings.Set("key2", "value4"));
        }
        [Fact]
        public void Can_lock_all_default_settings()
        {
            var settings = new Settings();
            settings.SetDefault("key", "value");
            settings.SetDefault("key2", "value2");
            settings.LockAll();
            Assert.Throws<SettingLockedException>(() => settings.SetDefault("key", "value3"));
            Assert.Throws<SettingLockedException>(() => settings.SetDefault("key2", "value4"));
        }
        [Fact]
        public void Can_lock_all_mixed_settings()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.Set("key2", "value2");
            settings.Set("key3", "value3");
            settings.SetDefault("key3", "value3");
            settings.SetDefault("defaultKey", "defaultValue");
            settings.SetDefault("defaultKey2", "defaultValue2");
            settings.LockAll();
            Assert.Throws<SettingLockedException>(() => settings.Set("key", "value3"));
            Assert.Throws<SettingLockedException>(() => settings.Set("key2", "value4"));
            Assert.Throws<SettingLockedException>(() => settings.Set("key3", "value3"));
            Assert.Throws<SettingLockedException>(() => settings.SetDefault("defaultKey", "defaultValue"));
            Assert.Throws<SettingLockedException>(() => settings.SetDefault("defaultKey2", "defaultValue2"));
            Assert.Throws<SettingLockedException>(() => settings.SetDefault("key3", "value3"));
        }

        [Fact]
        public void Locking_unexisting_key_has_no_effect()
        {
            var settings = new Settings();
            settings.Lock("key");
            settings.Set("key", "value");
            settings.SetDefault("key", "value");
            Assert.Equal(settings.Get("key"), "value");
        }
        [Fact]
        public void Locking_all_unexisting_keys_has_no_effect()
        {
            var settings = new Settings();
            settings.LockAll();
            settings.Set("key", "value");
            settings.Set("key2", "value2");
            settings.SetDefault("key", "value");
            settings.SetDefault("key2", "value2");
            Assert.Equal(settings.Get("key"), "value");
            Assert.Equal(settings.Get("key2"), "value2");
        }
        [Fact]
        public void Locking_all_existing_keys_has_effect_and_unexisting_has_no_effect()
        {
            var settings = new Settings();
            settings.Set("key", "value");
            settings.LockAll();
            settings.Set("key2", "value2");
            settings.Set("key2","value3");
            Assert.Throws<SettingLockedException>(() => settings.Set("key", "value"));
            Assert.Equal(settings.Get("key2"), "value3");
            
        }

    }
}
