using MessageHandler.EventProcessing.Runtime.Serialization;
using Xunit;

namespace unittests
{
    public class When_serializing_arrays
    {
        const string json = "[{\"SomeProperty\":\"test1\"},{\"SomeProperty\":\"test2\"}]";

        [Fact]
        public void Can_serialize_array()
        {
            var deserialized = new [] { new SerializedObject { SomeProperty = "test1" }, new SerializedObject { SomeProperty = "test2" } };

            var serialized = Json.Encode(deserialized);

            Assert.True(serialized == json);
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }

    }
}