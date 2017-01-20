using System;
using System.Collections.Generic;
using MessageHandler.EventProcessing.Runtime.Serialization;
using Xunit;

namespace unittests.Serialization
{
    public class When_working_with_dynamic_json_arrays
    {
        const string json = "[{\"SomeProperty\":\"test\"},{\"SomeProperty\":\"test\"}]";

        [Fact]
        public void Can_cast_to_typed_object_array()
        {
            SerializedObject[] deserialized = Json.Decode(json);

            Assert.NotNull(deserialized);
        }

        [Fact]
        public void Can_cast_to_object_array()
        {
            object[] deserialized = Json.Decode(json);

            Assert.NotNull(deserialized);
        }

        [Fact]
        public void Can_cast_array()
        {
            Array deserialized = Json.Decode(json);

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

        [Fact]
        public void Can_iterate_the_array_as_enumerable_object()
        {
            IEnumerable<object> deserialized = Json.Decode(json);

            var iterated = false;
            foreach (var item in deserialized)
            {
                iterated = true;
            }

            Assert.True(iterated);
        }

        [Fact]
        public void Can_get_objects_by_index()
        {
            dynamic deserialized = Json.Decode(json);

            Assert.True(deserialized[0].SomeProperty == "test");
            Assert.True(deserialized[1].SomeProperty == "test");

        }

        [Fact]
        public void Can_set_objects_by_index()
        {
            dynamic deserialized = Json.Decode(json);

            deserialized[0] = new SerializedObject
            {
                SomeProperty = "test2"
            };

            Assert.True(deserialized[0].SomeProperty == "test2");
            Assert.True(deserialized[1].SomeProperty == "test");

        }



        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }

    }
}