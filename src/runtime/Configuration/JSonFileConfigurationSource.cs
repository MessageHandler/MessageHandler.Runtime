using System.IO;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime.Serialization;

namespace MessageHandler.EventProcessing.Runtime.Configuration
{
    public class JSonFileConfigurationSource : IConfigurationSource
    {
        public Task<T> GetConfiguration<T>() where T : class, new()
        {
            return GetConfiguration<T>("handler.config.json");
        }

        public async Task<T> GetConfiguration<T>(string filename) where T : class, new()
        {
            using (var reader = File.OpenText(filename))
            {
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                return Json.Decode<T>(json);
            }
        }
    }
}