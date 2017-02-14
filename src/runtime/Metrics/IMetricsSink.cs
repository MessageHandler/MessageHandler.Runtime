using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IMetricsSink
    {
        void Add(Metric metric);

        Task Flush();
    }
}
