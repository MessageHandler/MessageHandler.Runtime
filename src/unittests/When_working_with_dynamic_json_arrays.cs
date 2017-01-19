using MessageHandler.EventProcessing.Runtime.Serialization;
using Xunit;

namespace unittests
{
    public class When_working_with_dynamic_json_arrays
    {
        const string json = "[{\"SomeProperty\":\"test\"},{\"SomeProperty\":\"test\"}]";

        [Fact]
        public void Can_cast_to_typed_object_array_typeof()
        {
            SerializedObject[] deserialized = Json.Decode(json);

            Assert.NotNull(deserialized);
        }

        [Fact]
        public void Can_iterate_the_array_dynamically()
        {
            dynamic deserialized = Json.Decode(json);

            var iterated = false;
            foreach (var item in deserialized)
            {
                Assert.True(item.SomeProperty == "test");
                iterated = true;
            }

            Assert.True(iterated);
        }

        [Fact]
        public void Can_iterate_the_array_statically()
        {
            SerializedObject[] deserialized = Json.Decode(json);

            var iterated = false;
            foreach (var item in deserialized)
            {
                Assert.True(item.SomeProperty == "test");
                iterated = true;
            }

            Assert.True(iterated);
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }

    }
}