using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IMetricsSink
    {
        Task Buffer(Metric metric);

        Task Flush();
    }
}
