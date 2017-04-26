using System.Threading.Tasks;

namespace MessageHandler.Runtime.Configuration
{
    public interface IDynamicConfigurationSource
    {
        Task<T> GetConfiguration<T>() where T : class, new();
        Task<T> GetConfiguration<T>(string filename) where T : class, new();
    }
}