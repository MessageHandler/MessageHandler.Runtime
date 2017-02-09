using System.Threading.Tasks;

namespace MessageHandler.EventProcessing.Runtime
{
    public interface IConvention
    {
        Task Apply(HandlerRuntimeConfiguration configuration);
    }
}