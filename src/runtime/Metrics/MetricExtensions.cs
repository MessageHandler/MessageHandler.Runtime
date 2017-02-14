using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class MetricExtensions
    {
        public static void RegisterMetricSink(this HandlerRuntimeConfiguration configuration, IMetricsSink sink)
        {
            var settings = configuration.GetSettings();
            var metrics = settings.GetOrCreate<MetricSinkTypes>();
            metrics.Add(sink.GetType());
            var container = settings.GetContainer();
            container.Register(() => sink);
        }

        public static void RegisterMetricSink(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var metrics = settings.GetOrCreate<MetricSinkTypes>();
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
