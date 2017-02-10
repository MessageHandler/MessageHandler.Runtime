using MessageHandler.Runtime.Serialization;
using Xunit;

namespace unittests.Serialization
{
    public class When_deserializing_arrays
    {
        const string json = "[{\"SomeProperty\":\"test1\"},{\"SomeProperty\":\"test2\"}]";

        [Fact]
        public void Can_deserialize_to_typed_object_array_generic()
        {
            var deserialized = Json.Decode<SerializedObject[]>(json);

            Assert.True(deserialized.Length == 2);
        }

        [Fact]
        public void Can_deserialize_to_typed_object_array_typeof()
        {
            SerializedObject[] deserialized = Json.Decode(json, typeof(SerializedObject[]));

            Assert.True(deserialized.Length == 2);
        }

        [Fact]
        public void Can_deserialize_to_dynamic_object_array()
        {
            dynamic deserialized = Json.Decode(json);

            Assert.True(deserialized.Length == 2);
        }
        
        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }
        
    }
}