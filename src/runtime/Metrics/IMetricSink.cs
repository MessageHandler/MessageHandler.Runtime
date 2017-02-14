using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IMetricSink
    {
        void Add(Metric metric);

        Task Flush();
    }

    public class Metric
    {
        public double Value { get; private set; }
        public string Name { get; private set; }
        public Metric(string name, double value)
        {
            Value = value;
            Name = name;
        }
    }
}
