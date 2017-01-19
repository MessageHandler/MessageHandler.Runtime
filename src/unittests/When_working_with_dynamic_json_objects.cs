using System;
using System.Collections.Generic;
using MessageHandler.EventProcessing.Runtime.Serialization;
using Xunit;

namespace unittests
{
    public class When_working_with_dynamic_json_objects
    {
        const string json = "{\"SomeProperty\":\"test\"}";

        [Fact]
        public void Can_cast_to_typed_object()
        {
            SerializedObject deserialized = Json.Decode(json);

            Assert.NotNull(deserialized);
        }

        [Fact]
        public void Can_cast_to_dictionary()
        {
            IDictionary<string, object> deserialized = Json.Decode(json);

            Assert.True((string)deserialized["SomeProperty"] == "test");
        }

        [Fact]
        public void Can_access_property_dynamically()
        {
            dynamic deserialized = Json.Decode(json);

            Assert.True(deserialized.SomeProperty == "test");
        }

        [Fact]
        public void Can_access_property_like_dictionary()
        {
            dynamic deserialized = Json.Decode(json);

            Assert.True(deserialized["SomeProperty"] == "test");
        }

        [Fact]
        public void Can_set_property_dynamically()
        {
            dynamic deserialized = Json.Decode(json);

            deserialized.SomeProperty = "test2";

            Assert.True(deserialized.SomeProperty == "test2");
        }

        [Fact]
        public void Can_set_property_like_dictionary()
        {
            dynamic deserialized = Json.Decode(json);

            deserialized["SomeProperty"] = "test2";

            Assert.True(deserialized["SomeProperty"] == "test2");
        }

        [Fact]
        public void Can_convert_to_dictionary()
        {
            dynamic deserialized = Json.Decode(json);

            var dict = deserialized.ToDictionary();

            Assert.True(dict["SomeProperty"] == "test");
        }



        [Theory]
        //[InlineData("1997")] //not automatically converted because to much conflict with numbers
        [InlineData("1997-07")]
        [InlineData("1997-07-16")]
        [InlineData("1997-07-16T19:20+01:00")]
        [InlineData("1997-07-16T19:20:30+01:00")]
        [InlineData("1997-07-16T19:20:30.45+01:00")]
        public void Can_deal_with_iso_date_formats(string date)
        {
            dynamic deserialized = Json.Decode("{Date:\"" + date + "\"}");

            Assert.True(deserialized.Date is DateTime);
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }



    }
}