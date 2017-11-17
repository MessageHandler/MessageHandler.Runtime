using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IMetrics
    {
        MetricsCompletionBehavior DefaultCompletionBehavior { get; set; }

        Task Track(Metric measured);
        Task Track(Metric measured, MetricsCompletionBehavior completionBehavior);

        Task Start(CancellationToken token);
        Task Stop();

    }
}