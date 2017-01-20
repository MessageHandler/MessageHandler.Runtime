using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime.Configuration;
using MessageHandler.EventProcessing.Runtime.Serialization;
using Xunit;

namespace unittests.Serialization
{
    public class When_getting_configuration
    {
        const string json = "[{\"SomeProperty\":\"test1\"},{\"SomeProperty\":\"test2\"}]";

        [Fact]
        public async Task Can_get_object_from_file()
        {
            var configurationSource = new JSonFileConfigurationSource();

            var deserialized = await configurationSource.GetConfiguration<SerializedObject>();

            Assert.NotNull(deserialized);
            Assert.True(deserialized.SomeProperty == "test");
        }
        
        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }
    }
}