using System.Configuration;
using MessageHandler.Runtime.Serialization;
using Xunit;

namespace unittests.Serialization
{
    public class When_deserializing_objects
    {
        const string json = "{\"SomeProperty\":\"test\"}";

        [Fact]
        public void Can_deserialize_to_typed_object_generic()
        {
            var deserialized = Json.Decode<SerializedObject>(json);

            Assert.True(deserialized.SomeProperty == "test");
        }

        [Fact]
        public void Can_deserialize_to_typed_object_typeof()
        {
            SerializedObject deserialized = Json.Decode(json, typeof(SerializedObject));

            Assert.True(deserialized.SomeProperty == "test");
        }

        [Fact]
        public void Can_deserialize_to_dynamic_object()
        {
            dynamic deserialized = Json.Decode(json);

            Assert.True(deserialized.SomeProperty == "test");
        }

        [Fact]
        public void Can_deserialize_to_configuration_section()
        {
            var deserialized = Json.Decode<SerializedConfigSection>(json);

            Assert.True(deserialized.SomeProperty == "test");
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }

        public class SerializedConfigSection : ConfigurationSection
        {
            [ConfigurationProperty("SomeProperty", IsRequired = false)]
            public string SomeProperty
            {
                get { return this["SomeProperty"] as string; }
                set { this["SomeProperty"] = value; }
            }
        }
    }
}
