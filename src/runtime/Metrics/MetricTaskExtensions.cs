using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class MetricTaskExtensions
    {
        public static void RegisterMetricSink(this HandlerRuntimeConfiguration configuration, IMetricSink metric)
        {
            var settings = configuration.GetSettings();
            var metrics = settings.GetOrCreate<MetricTypes>();
            metrics.Add(metric.GetType());
            var container = settings.GetContainer();
            container.Register(() => metric);
        }

        public static void RegisterMetricSink(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var metrics = settings.GetOrCreate<MetricTypes>();
            var container = settings.GetContainer();
            container.Register(type);
            metrics.Add(type);
        }

        public static void RegisterMetricSink<T>(this HandlerRuntimeConfiguration configuration)
        {
            configuration.RegisterMetricSink(typeof(T));
        }
    }
}
