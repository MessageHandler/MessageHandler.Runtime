using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IConvention
    {
        Task Apply(ConfigurationRoot configuration);
    }
}