using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.Metrics
{
    public class When_registering_metrics
    {
        [Fact]
        public void Can_register_metric_and_find_it_in_the_container()
        {
            var myMetric = new MyMetric();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterMetricSink(myMetric);
            Assert.NotNull(container.Resolve<IMetricSink>());
            Assert.IsType<MyMetric>(container.Resolve<IMetricSink>());
        }

        [Fact]
        public void Can_register_metric_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var myMetric = new MyMetric();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.RegisterMetricSink(myMetric);
            var metrics = settings.Get<MetricTypes>();
            Assert.NotNull(metrics.Exists(t => t == typeof(MyMetric)));
        }

        [Fact]
        public void Can_register_metric_instance_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterMetricSink<MyMetric>();
            Assert.NotNull(container.Resolve<IMetricSink>());
            Assert.IsType<MyMetric>(container.Resolve<IMetricSink>());
        }

        [Fact]
        public void Can_register_metric_instance_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterMetricSink(typeof(MyMetric));
            Assert.NotNull(container.Resolve<IMetricSink>());
            Assert.IsType<MyMetric>(container.Resolve<IMetricSink>());
        }
    }

    public class MyMetric:IMetricSink
    {
        public bool AddIsCalled;
        public void Add(Metric metric)
        {
            AddIsCalled = true;
        }

        public Task Flush()
        {
            return null;
        }
    }
}
