using System.IO;
using System.Threading.Tasks;
using MessageHandler.Runtime.Configuration;
using Xunit;

namespace unittests.Configuration
{
    public class When_getting_configuration
    {
        [Fact]
        public async Task Can_get_object_from_file()
        {
            var configurationSource = new JSonFileConfigurationSource();

            var deserialized = await configurationSource.GetConfiguration<SerializedObject>().ConfigureAwait(false);

            Assert.NotNull(deserialized);
            Assert.True(deserialized.SomeProperty == "test");
        }
        
        [Fact]
        public async Task Throws_file_not_found_exception_if_file_does_not_exist()
        {
            var configurationSource = new JSonFileConfigurationSource("unexisting.json");

            await Assert.ThrowsAsync<FileNotFoundException>(() => configurationSource.GetConfiguration<SerializedObject>()).ConfigureAwait(false);
        }

        public class SerializedObject
        {
            public string SomeProperty { get; set; }
        }
    }
}