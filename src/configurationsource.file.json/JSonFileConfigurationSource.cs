using System.IO;
using System.Threading.Tasks;
using MessageHandler.Runtime.Serialization;

namespace MessageHandler.Runtime.Configuration
{
    public class JSonFileConfigurationSource : IConfigurationSource
    {
        private readonly string _filename = "handler.config.json";

        public JSonFileConfigurationSource() { }

        public JSonFileConfigurationSource(string filename)
        {
            _filename = filename;
        }
        public async Task<T> GetConfiguration<T>() where T : class, new()
        {
            using (var reader = File.OpenText(_filename))
            {
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                return Json.Decode<T>(json);
            }
        }
    }
}