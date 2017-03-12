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
            var myMetric = new MyMetricSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Metrics().RegisterSink(myMetric);
            Assert.NotNull(container.Resolve<IMetricsSink>());
            Assert.IsType<MyMetricSink>(container.Resolve<IMetricsSink>());
        }

        [Fact]
        public void Can_register_metric_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var myMetric = new MyMetricSink();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.Metrics().RegisterSink(myMetric);
            var metrics = settings.Get<MetricSinkTypes>();
            Assert.NotNull(metrics.Exists(t => t == typeof(MyMetricSink)));
        }

        [Fact]
        public void Can_register_metric_instance_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Metrics().RegisterSink<MyMetricSink>();
            Assert.NotNull(container.Resolve<IMetricsSink>());
            Assert.IsType<MyMetricSink>(container.Resolve<IMetricsSink>());
        }

        [Fact]
        public void Can_register_metric_instance_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Metrics().RegisterSink(typeof(MyMetricSink));
            Assert.NotNull(container.Resolve<IMetricsSink>());
            Assert.IsType<MyMetricSink>(container.Resolve<IMetricsSink>());
        }
    }

    public class MyMetricSink:IMetricsSink
    {
        public Task Buffer(Metric metric)
        {
            return Task.CompletedTask;
        }

        public Task Flush()
        {
            return null;
        }
    }
}
