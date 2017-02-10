using MessageHandler.Runtime.Serialization;
using Xunit;

namespace unittests.Serialization
{
    public class When_serializing_objects
    {
        const string json = "{\"SomeProperty\":\"test\"}";

        [Fact]
        public void Can_serialize_object()
        {
            var deserialized = new SerializedObject {SomeProperty = "test"};

            var serialized = Json.Encode(deserialized);

            Assert.True(serialized == json);
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }
        
    }
}