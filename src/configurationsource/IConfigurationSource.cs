using System.Threading.Tasks;

namespace MessageHandler.Runtime.Configuration
{
    public interface IConfigurationSource
    {
        Task<T> GetConfiguration<T>() where T : class, new();
    }
}
