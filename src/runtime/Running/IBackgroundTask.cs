using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.EventProcessing.Runtime
{
    public interface IBackgroundTask
    {
        Task Run(CancellationToken cancellation);
    }
}