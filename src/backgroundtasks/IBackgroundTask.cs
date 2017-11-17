using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IBackgroundTask
    {
        Task Run(CancellationToken cancellation);
    }
}