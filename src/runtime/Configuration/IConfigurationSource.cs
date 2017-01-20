using System.Threading.Tasks;

namespace MessageHandler.EventProcessing.Runtime.Configuration
{
    public interface IConfigurationSource
    {
        Task<T> GetConfiguration<T>() where T : class, new();
        Task<T> GetConfiguration<T>(string filename) where T : class, new();
    }
}
