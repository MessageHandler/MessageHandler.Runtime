using MessageHandler.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationSettings
{
    public class When_working_with_types
    {
        [Fact]
        public void Can_get_typed_value()
        {
            var settings = new Settings();
            var testValue = new TestValue {Text = "test"};
            settings.Set("key", testValue);
            Assert.StrictEqual(settings.Get<TestValue>("key"), testValue);
        }

        [Fact]
        public void Can_get_typed_value_by_generic()
        {
            var settings = new Settings();
            var testValue = new TestValue { Text = "test" };
            settings.Set(testValue);
            Assert.StrictEqual(settings.Get<TestValue>(), testValue);
        }

        [Fact]
        public void Can_get_typed_value_by_type()
        {
            var settings = new Settings();
            var testValue = new TestValue { Text = "test" };
            settings.Set(testValue);
            Assert.StrictEqual(settings.Get(typeof(TestValue)), testValue);
        }

        [Fact]
        public void Can_get_typed_default_value_by_type()
        {
            var settings = new Settings();
            var testValue = new TestValue { Text = "test" };
            settings.SetDefault(testValue);
            Assert.StrictEqual(settings.GetDefault(typeof(TestValue)), testValue);
        }

        [Fact]
        public void Can_set_typed_value()
        {
            var settings = new Settings();
            var testValue = new TestValue { Text = "test" };
            settings.Set(testValue);
            Assert.StrictEqual(settings.Get<TestValue>(typeof(TestValue).FullName),testValue);
        }
    
        [Fact]
        public void Can_get_typed_default_value()
        {
            var settings = new Settings();
            var testValue = new TestValue { Text = "test" };
            settings.SetDefault("key", testValue);
            Assert.StrictEqual(settings.GetDefault<TestValue>("key"), testValue);
        }

        [Fact]
        public void Can_set_typed_default_value()
        {
            var settings = new Settings();
            var testValue = new TestValue { Text = "test" };
            settings.SetDefault(testValue);
            Assert.StrictEqual(settings.GetDefault<TestValue>(typeof(TestValue).FullName), testValue);
        }

        [Fact]
        public void Can_remove_typed_value()
        {
            var settings = new Settings();
            var testValue = new TestValue();
            settings.Set(testValue);
            settings.Remove<TestValue>();
            Assert.Null(settings.Get<TestValue>());
        }

        [Fact]
        public void Can_remove_typed_default_value()
        {
            var settings = new Settings();
            var testValue = new TestValue();
            settings.SetDefault(testValue);
            settings.Remove<TestValue>();
            Assert.Null(settings.GetDefault<TestValue>());
        }

        [Fact]
        public void Can_remove_typed_value_by_type()
        {
            var settings = new Settings();
            var testValue = new TestValue();
            settings.Set(testValue);
            settings.Remove(typeof(TestValue));
            Assert.Null(settings.Get<TestValue>());
        }

        [Fact]
        public void Can_remove_typed_default_value_by_type()
        {
            var settings = new Settings();
            var testValue = new TestValue();
            settings.SetDefault(testValue);
            settings.Remove(typeof(TestValue));
            Assert.Null(settings.GetDefault<TestValue>());
        }
        
        public class TestValue
        {
            public string Text { get; set; }
        }
    }
}
