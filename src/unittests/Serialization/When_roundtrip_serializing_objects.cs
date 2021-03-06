using MessageHandler.Runtime.Serialization;
using Xunit;

namespace unittests.Serialization
{
    public class When_roundtrip_serializing_objects
    {
        [Fact]
        public void Can_serialize_a_deserialized_object()
        {
            var deserialized1 = new SerializedObject { SomeProperty = "test" };

            var serialized1 = Json.Encode(deserialized1);

            var deserialized2 = Json.Decode(serialized1);

            var serialized2 = Json.Encode(deserialized2);

            Assert.True(serialized1 == serialized2);
        }

        [Fact]
        public void Can_serialize_a_complex_deserialized_object()
        {
            var deserialized1 = new ComplexSerializedObject()
            {
                SomeProperty = "test",
                Children = new[]
                {
                    new SerializedObject {SomeProperty = "test"}
                },
                Child = new SerializedObject {SomeProperty = "test"}
            };

            var serialized1 = Json.Encode(deserialized1);

            var deserialized2 = Json.Decode(serialized1);

            var serialized2 = Json.Encode(deserialized2);

            Assert.True(serialized1 == serialized2);
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }

        public class ComplexSerializedObject
        {
            public string SomeProperty { get; set; }

            public SerializedObject[] Children { get; set; }

            public SerializedObject Child { get; set; }
        }

    }
}