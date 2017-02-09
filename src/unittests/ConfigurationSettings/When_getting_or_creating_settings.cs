using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationSettings
{
    public class When_getting_or_creating_settings
    {
        [Fact]
        public void Will_create_setting_if_not_existing()
        {
            var settings = new Settings();
            var setting = settings.GetOrCreate(typeof(MySetting));
            Assert.NotNull(setting);
        }

        [Fact]
        public void Will_return_setting_if_exist()
        {
            var settings = new Settings();
            var setting1 = settings.GetOrCreate(typeof(MySetting));
            var setting2 = settings.GetOrCreate(typeof(MySetting));
            Assert.StrictEqual(setting1, setting2);
        }

        [Fact]
        public void Will_create_setting_if_not_existing_generic()
        {
            var settings = new Settings();
            var setting = settings.GetOrCreate<MySetting>();
            Assert.NotNull(setting);
        }

        [Fact]
        public void Will_create_setting_if_not_existing_with_key()
        {
            var settings = new Settings();
            var setting = settings.GetOrCreate("key", typeof(MySetting));
            Assert.NotNull(setting);
        }

        public class MySetting
        {
            
        }
    }
}
